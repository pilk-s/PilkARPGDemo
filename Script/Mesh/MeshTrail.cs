using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float activeTime = 2f;
    [Header("Mesh 相关参数")]
    public float meshRefreshRate = 0.1f;
    public Transform meshRootTransform;
    public Transform positionToSpawn;
    private float destroyMeshDelay = 3f;
    
    [SerializeField, Header("Shader 相关参数")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate=0.1f;
    public float shaderVarRefreshRate=0.05f;
    
    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    void Update()
    {
        if (GameInputManager.MainInstance.Trail&&!isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive>0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject go = new GameObject();
                go.transform.parent = meshRootTransform;
                go.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);
                MeshRenderer mr = go.AddComponent<MeshRenderer>();
                MeshFilter mf = go.AddComponent<MeshFilter>();

                Mesh mesh =new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);
                mf.mesh = mesh;
                
                Material[] tempmrs =new Material[mesh.subMeshCount];
                for (var j = 0; j < tempmrs.Length; j++)
                {
                    tempmrs[j] = mat;
                }
                mr.materials = tempmrs;

                for (int k = 0; k < mesh.subMeshCount; k++)
                {
                    StartCoroutine(AnimateMaterialFloat(mr.materials[k],0f,
                        shaderVarRate,shaderVarRefreshRate));
                }
                               
                Destroy(go,destroyMeshDelay);

            }
            
            yield return new WaitForSeconds(meshRefreshRate);
        }
        
        isTrailActive = false; 
    }

    IEnumerator AnimateMaterialFloat(Material mat,float goal,float rate,float refreshRate)
    {
        var ValueToAnimate = mat.GetFloat(shaderVarRef);
        while (ValueToAnimate > goal)
        {
            ValueToAnimate -= rate;
            mat.SetFloat(shaderVarRef,ValueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
        yield break;
    }
}
