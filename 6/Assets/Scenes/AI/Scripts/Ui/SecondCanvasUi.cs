namespace Scenes.AI.Ui
{
    using UnityEngine;

    public class SecondCanvasUi : NavigationalCanvas
    {

        public void GoToThird()
        {
            NavigationalCanvasManager.Instance.Show("third");
        }
    }
}