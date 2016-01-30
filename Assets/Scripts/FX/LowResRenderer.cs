using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class LowResRenderer : MonoBehaviour
{
    public RawImage Renderer;

    RenderTexture[] t;

    void Awake()
    {
        // 16:9 -> 320x180
        //  4:3 -> 320x240
        int newWidth = 320;
        int newHeight = 180;
        if (Mathf.Approximately(Screen.width / Screen.height, 4f / 3f))
            newHeight = 240;

        Renderer.gameObject.SetActive(false);

        Camera[] cams = FindObjectsOfType<Camera>().Where(cam => cam.enabled).OrderByDescending(cam => cam.depth).ToArray();
        t = new RenderTexture[cams.Length];
        for (int i=0; i<cams.Length; ++i)
        {
            t[i] = new RenderTexture(newWidth, newHeight, 24, RenderTextureFormat.ARGB32);
            t[i].filterMode = FilterMode.Point;
            cams[i].targetTexture = t[i];

            RawImage ri = Instantiate(Renderer);
            ri.transform.SetParent(Renderer.transform.parent, false);
            ri.transform.SetAsFirstSibling();
            ri.texture = t[i];
            ri.gameObject.SetActive(true);
        }
    }
}
