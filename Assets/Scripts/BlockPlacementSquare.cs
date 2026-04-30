using UnityEngine;

public class BlockPlacementSquare : MonoBehaviour
{
    public string correctBlockTag;

    public Renderer squareRenderer;
    public Material correctMaterial;
    public Material wrongMaterial;

    private GameObject currBlock;
    private bool isCorrect = false;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag(correctBlockTag))
        {
            currBlock = other.gameObject;
            isCorrect = true;
            Debug.Log(other.name + "placed on the correct sqaure");

            if (squareRenderer != null && correctMaterial != null){
                squareRenderer.material = correctMaterial;
            }
            else
            {
                Debug.Log(other.name + " is on the wrong sqaure.");

                if(squareRenderer != null && wrongMaterial != null)
                {
                    squareRenderer.material = wrongMaterial;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.gameObject == currBlock)
        {
            currBlock = null;
            isCorrect = false;

            Debug.Log("Correct block removed from square.");
        }
    }
    public bool IsCorrect(){
        return isCorrect;

    }
}

