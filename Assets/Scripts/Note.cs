[System.Serializable]
public class Note
{
    public enum NoteType
    {
        Tap,
    }

    public TapObjectType tapObject;

    public NoteType type;

    public float time;
}