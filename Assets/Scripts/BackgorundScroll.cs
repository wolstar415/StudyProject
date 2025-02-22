using UnityEngine;

public class BackgorundScroll : MonoBehaviour
{
    public float scrollSpeed;

    public MeshRenderer meshRenderer;



    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.Instance.CalculateGameSpeed()  / 20f * Time.deltaTime, 0);
    }
}
