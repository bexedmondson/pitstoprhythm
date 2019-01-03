[System.Serializable]
public class NoteData
{
    public enum NoteType
    {
        Tap,
    }

    public TapObjectType tapObject;

    public NoteType type;

    public float time;
}