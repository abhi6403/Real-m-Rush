using UnityEngine;
using UnityEngine.Rendering;

public class RenderPipelineChecker : MonoBehaviour
{
    void Start()
    {
        if (GraphicsSettings.currentRenderPipeline)
        {
            string pipelineTypeName = GraphicsSettings.currentRenderPipeline.GetType().ToString();

            if (pipelineTypeName.Contains("HighDefinition"))
            {
                Debug.Log("Project is using High Definition Render Pipeline (HDRP).");
            }
            else if (pipelineTypeName.Contains("Universal"))
            {
                Debug.Log("Project is using Universal Render Pipeline (URP).");
            }
            else
            {
                Debug.Log("Project is using a custom Scriptable Render Pipeline or an unknown SRP.");
            }
        }
        else
        {
            Debug.Log("Project is using the Built-in Render Pipeline.");
        }
    }
}