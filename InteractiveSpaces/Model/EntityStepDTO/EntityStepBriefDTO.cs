namespace InteractiveSpaces.Model.EntityStepDTO
{
    public class EntityStepBriefDTO
    {
        public EntityStepBriefDTO(int stepDescriptionId, int entityId, float x, float y, float z, float rotX, float rotY, float rotZ, float scaleX, float scaleY, float scaleZ, IList<ActionEntityStepBriefDTO>? hasActions)
        {
            StepDescriptionId = stepDescriptionId;
            EntityId = entityId;
            X = x;
            Y = y;
            Z = z;
            RotX = rotX;
            RotY = rotY;
            RotZ = rotZ;
            ScaleX = scaleX;
            ScaleY = scaleY;
            ScaleZ = scaleZ;
            HasActions = hasActions;
        }

        public int StepDescriptionId { get; set; }
        public int EntityId { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float RotX { get; set; }

        public float RotY { get; set; }

        public float RotZ { get; set; }

        public float ScaleX { get; set; }

        public float ScaleY { get; set; }

        public float ScaleZ { get; set; }

        public IList<ActionEntityStepBriefDTO>? HasActions { get; set; }
    }

    public class ActionEntityStepBriefDTO
    {
        public ActionEntityStepBriefDTO()
        {
        }

        public ActionEntityStepBriefDTO(ActionEntityStep action)
        {
            Description = action.Description;
            FeedbackMessage = action.FeedbackMessage;
            ActionType = action.ActionType;
            AnimationId = action.AnimationId;
        }

        public string? Description { get; set; }
        public string? FeedbackMessage { get; set; }

        [EnumDataType(typeof(TypeOfActionEntityStep))]
        public TypeOfActionEntityStep ActionType { get; set; }
        public int AnimationId { get; set; }
    }
}
