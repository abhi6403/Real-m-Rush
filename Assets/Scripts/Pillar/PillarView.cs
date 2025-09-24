using RealmRush.Player;
using TMPro;
using UnityEngine;

public class PillarView : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    private int _pillarNumber;

    public void AsignPillarNumber(int pillarNumber)
    {
        _pillarNumber = pillarNumber;
        Debug.Log(_pillarNumber);
        numberText.text = _pillarNumber.ToString();
    }

    public void CheckWithPlayer(PlayerView player)
    {
        if (_pillarNumber - player.GetCurrentPillarNumber()  == 1 || player.GetCurrentPillarNumber() == 0)
        {
            Debug.Log("Good");
            player.SetCurrentPillarNumber(_pillarNumber);
        }
        else
        {
            Debug.Log("Bad");
        }
    }
}
