using UnityEngine;

//[CreateAssetMenu]
public class ResourcesSO : ScriptableObject
{
    [SerializeField]
    private int _Value;

    public int Value
    {
        get { return _Value; }
        set { _Value = value; }
    }
}
