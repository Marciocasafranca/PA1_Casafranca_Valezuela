using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleted)
            return;

        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            Debug.Log("¡NIVEL COMPLETADO!");

            Time.timeScale = 0f;
        }
    }

    private void OnGUI()
    {
        if (!levelCompleted)
            return;

        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);

        titleStyle.fontSize = 40;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = Color.white;
        titleStyle.fontStyle = FontStyle.Bold;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);

        GUI.Box(
            new Rect(
                Screen.width / 2 - 250,
                Screen.height / 2 - 100,
                500,
                200
            ),
            ""
        );

        GUI.Label(
            new Rect(
                Screen.width / 2 - 250,
                Screen.height / 2 - 70,
                500,
                100
            ),
            "¡NIVEL COMPLETADO!",
            titleStyle
        );
    }
}