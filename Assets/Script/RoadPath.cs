using UnityEngine;

public class VehiclePathMovement : MonoBehaviour
{
    [Header("Points du Chemin")]
    public Transform[] pathPoints; // Tableau des points du chemin
    public float speed = 5f; // Vitesse du v�hicule

    private int currentPointIndex = 0; // Index du point actuel

    void Start()
    {
        if (pathPoints == null || pathPoints.Length == 0) return;

        // Positionner et orienter le v�hicule au point de d�part (point 0)
        transform.position = pathPoints[0].position;
        transform.rotation = pathPoints[0].rotation;
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Length == 0) return;

        // Obtenir le point actuel et suivant
        Transform currentPoint = pathPoints[currentPointIndex];
        Transform nextPoint = pathPoints[(currentPointIndex + 1) % pathPoints.Length];

        // D�placer le v�hicule vers le prochain point
        MoveTowardsPoint(nextPoint);

        // Passer au point suivant lorsque le v�hicule atteint le point actuel
        if (Vector3.Distance(transform.position, nextPoint.position) < 0.1f)
        {
            // Configurer l'orientation exacte au point
            transform.rotation = nextPoint.rotation;
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length; // Boucle vers le point suivant
        }
    }

    /// <summary>
    /// D�place le v�hicule vers le prochain point.
    /// </summary>
    private void MoveTowardsPoint(Transform targetPoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
    }

    /// <summary>
    /// Dessine les points et le chemin dans la sc�ne pour le d�bogage.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < pathPoints.Length; i++)
        {
            Gizmos.DrawSphere(pathPoints[i].position, 0.2f); // Dessiner les points du chemin
            Gizmos.DrawLine(pathPoints[i].position, pathPoints[(i + 1) % pathPoints.Length].position); // Relier les points
        }
    }
}
