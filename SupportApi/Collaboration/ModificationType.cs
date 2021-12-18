namespace SupportApi.Collaboration
{

    /// <summary>
    /// Shared document modification type.
    /// </summary>
    public enum ModificationType
    {
        NoChanges = 0,
        Structure = 1,
        RemoveAnnotation = 2,
        AddAnnotation = 3,
        UpdateAnnotation = 4,
        Undo = 5,
        Redo = 6,
        ResetUndo = 7,
        Reset = 8,
    }

}