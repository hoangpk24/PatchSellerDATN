namespace PatchSeller.Web.Services
{
    public class DrawerService
    {
        public bool IsOpen { get; set; } = false;
        public bool IsOpenHeader { get; set; } = false;

        public event Action OnChange;

        public void OpenDrawer()
        {
            if (!IsOpen)
            {
                IsOpen = true;
                NotifyStateChanged();
            }
        }

        public void CloseDrawer()
        {
            if (IsOpen)
            {
                IsOpen = false;
                NotifyStateChanged();
            }
        }

        public void OpenHeaderDrawer()
        {
            if (!IsOpenHeader)
            {
                IsOpenHeader = true;
                NotifyStateChanged();
            }
        }

        public void CloseHeaderDrawer()
        {
            if (IsOpenHeader)
            {
                IsOpenHeader = false;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
