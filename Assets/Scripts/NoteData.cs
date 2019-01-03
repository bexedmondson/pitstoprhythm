[System.Serializable]
public class NoteData
{
    public enum NoteType
    {
		DragIn,
        Tap,
        DragOut,
    }

    public TapObjectType tapObject;

    public NoteType type;

    public float time;
}