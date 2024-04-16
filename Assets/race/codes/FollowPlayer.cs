using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private float rotationSpeed;
    private float distanceBehind; // Distance derrière la voiture
    private float heightAbove; // Hauteur au-dessus de la voiture
    private int presetIndex;// Index du preset de caméra actuel

    // Définir les presets de caméra. Chaque preset est un tableau de trois valeurs : la vitesse de rotation, la distance derrière la voiture et la hauteur au-dessus de la voiture
    private float[][] camPreset = new float[][] {
        new float[] { 5f, 5f, 2f }, // drone mode
        new float[] { 15f, 8f, 5f }, // 3rd person mode
        new float[] { 80f, 0.2f, 0f } // 1st person mode
    };

    // Start is called before the first frame update
    void Start()
    {
        presetIndex = 1;
        rotationSpeed = camPreset[presetIndex][0];
        distanceBehind = camPreset[presetIndex][1];
        heightAbove = camPreset[presetIndex][2];
    }

    public GameObject switchToInvisible()
    {
        presetIndex = -1;
        // drone view for the camera
        rotationSpeed = camPreset[0][0];
        distanceBehind = camPreset[0][1];
        heightAbove = camPreset[0][2];
        // set player to the invisible ai
        player = GameObject.Find("invisibleAI").transform;
        return player.gameObject;
    }

    public void switchToRace()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeCameraPreset(1);
    }

    public void switchToDrone()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeCameraPreset(0);
        presetIndex = -1;
    }

    void ChangeCameraPreset()
    {
        if (presetIndex == -1) return; // if we are in pre-mode we can't change the camera
        presetIndex++;
        if (presetIndex >= camPreset.Length)
        {
            presetIndex = 0;
        }
        
        if (presetIndex == 2) // 1st person mode
        {
            // hide the car
            foreach (MeshRenderer element in GameObject.Find("player").GetComponentsInChildren<MeshRenderer>())
            {
                element.enabled = false;
            }
        } else {
            // show the car
            foreach (MeshRenderer element in GameObject.Find("player").GetComponentsInChildren<MeshRenderer>())
            {
                element.enabled = true;
            }
        }
        rotationSpeed = camPreset[presetIndex][0];
        distanceBehind = camPreset[presetIndex][1];
        heightAbove = camPreset[presetIndex][2];
    }

    void ChangeCameraPreset(int index)
    {
        if (index < 0 || index >= camPreset.Length) return;
        presetIndex = index;
        rotationSpeed = camPreset[presetIndex][0];
        distanceBehind = camPreset[presetIndex][1];
        heightAbove = camPreset[presetIndex][2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeCameraPreset();
        }
        /*
        // Définir la position de la caméra par rapport au joueur avec un offset
        Vector3 desiredPosition = player.position - player.forward * distanceBehind + Vector3.up * heightAbove;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Calculer la rotation de la caméra en fonction de la rotation de la voiture
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        // Interpoler en douceur vers la nouvelle rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
    }

    void FixedUpdate() // make the camera moves on fixed update to avoid jittering
    {
        // Définir la position de la caméra par rapport au joueur avec un offset
        Vector3 desiredPosition = player.position - player.forward * distanceBehind + Vector3.up * heightAbove;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Calculer la rotation de la caméra en fonction de la rotation de la voiture
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        // Interpoler en douceur vers la nouvelle rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
