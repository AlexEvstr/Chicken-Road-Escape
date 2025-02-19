using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public float distanceBetweenStars;
    public float heightBetweenStars;
    public ObjectPooler starPool;

    public void spawnStars(Vector3 startPosition)
    {
        int pattern = Random.Range(0, 3); // 0 - горизонталь, 1 - вертикаль, 2 - треугольник
        int starCount = (pattern == 1) ? Random.Range(1, 6) : Random.Range(1, 4);

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = starPool.getPooledObject();
            if (star != null)
            {
                if (pattern == 0) // Горизонтальная линия
                {
                    star.transform.position = new Vector3(startPosition.x + (i * distanceBetweenStars), startPosition.y, startPosition.z);
                }
                else if (pattern == 1) // Вертикальная линия
                {
                    star.transform.position = new Vector3(startPosition.x, startPosition.y + (i * heightBetweenStars), startPosition.z);
                }
                else if (pattern == 2) // Треугольник
                {
                    star.transform.position = new Vector3(startPosition.x + (i * distanceBetweenStars) - (i / 2f * distanceBetweenStars), startPosition.y + (i * heightBetweenStars), startPosition.z);
                }
                star.SetActive(true);
            }
        }
    }
}
