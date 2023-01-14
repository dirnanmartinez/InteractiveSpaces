namespace InteractiveSpaces.Model.EntityStepDTO
{
    public class EntityStepDetail3DLocationDTO
    {
        public EntityStepDetail3DLocationDTO() { }

        public EntityStepDetail3DLocationDTO(EntityStep entity)
        {
            EntityPath = ((Entity3D)entity.Entity)==null?null:((Entity3D)entity.Entity).Path;
            EntityName = entity.Entity.Name;
            if (entity.LocatedIn != null) {
                X = ((Location3D)entity.LocatedIn).X;
                Y = ((Location3D)entity.LocatedIn).Y;
                Z = ((Location3D)entity.LocatedIn).Z;
                RotX = ((Location3D)entity.LocatedIn).RotX;
                RotY = ((Location3D)entity.LocatedIn).RotY;
                RotZ = ((Location3D)entity.LocatedIn).RotZ;
                ScaleX = ((Location3D)entity.LocatedIn).ScaleX;
                ScaleY = ((Location3D)entity.LocatedIn).ScaleY;
                ScaleZ = ((Location3D)entity.LocatedIn).ScaleZ;
            }

            Actions = entity.HasActions==null?null: entity.HasActions.Select(e=>new ActionEntityDetailDTO(e)).ToList();
        }

        public string? EntityPath { get; set; }
        public string EntityName { get; set; }
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float RotX { get; set; }

        public float RotY { get; set; }

        public float RotZ { get; set; }

        public float ScaleX { get; set; }

        public float ScaleY { get; set; }

        public float ScaleZ { get; set; }

        

        public IList<ActionEntityDetailDTO>? Actions { get; set; }

    }

    public class ActionEntityDetailDTO
    {
        public ActionEntityDetailDTO() { }

        public ActionEntityDetailDTO(ActionEntityStep action)
        {
            Description = action.Description;
            FeedbackMessage = action.FeedbackMessage;
            ActionType = action.ActionType;
            AnimationIdPrefab = action.Animation==null?null:action.Animation.AnimationId;
        }

        public string? Description { get; set; }
        public string? FeedbackMessage { get; set; }

        [EnumDataType(typeof(TypeOfActionEntityStep))]
        public TypeOfActionEntityStep ActionType { get; set; }

        public string? AnimationIdPrefab { get; set; }


    }
}
