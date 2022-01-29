namespace FilmCatalog.Models.ViewModels
{
    public static class PostViewModelFactory
    {
        public static PostViewModel View()
        {
            return new PostViewModel
            {
                Action = "Details",
                ActionRu = "Подробности",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false
            };
        }

        public static PostViewModel Create()
        {
            return new PostViewModel
            {
                Action = "Create",
                ActionRu = "Создавать",
                Theme = "primary",
                ShowPoster = false,
                ShowPosterInput = true,
                ShowRegistration = false,
                ShowBack = false
            };
        }

        public static PostViewModel Edit()
        {

            return new PostViewModel
            {
                Action = "Edit",
                ActionRu = "Редактировать",
                ShowPosterInput = true,
                ChangePoster = true,
                Theme = "warning",
                ShowRegistration = false,
            };
        }

        public static PostViewModel Delete()
        {
            return new PostViewModel
            {
                Action = "Delete",
                ActionRu = "Удалить",
                ReadOnly = true,
                Theme = "danger"
            };
        }
    }
}