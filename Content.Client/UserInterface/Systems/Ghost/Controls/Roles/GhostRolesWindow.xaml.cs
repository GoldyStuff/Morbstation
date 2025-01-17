using Content.Client.Redial;
using Content.Shared.Ghost.Roles;
using Content.Shared.CCVar;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Configuration;

namespace Content.Client.UserInterface.Systems.Ghost.Controls.Roles
{
    [GenerateTypedNameReferences]
    public sealed partial class GhostRolesWindow : DefaultWindow
    {
        public event Action<GhostRoleInfo>? OnRoleRequested;
        public event Action<GhostRoleInfo>? OnRoleFollow;

        public void ClearEntries()
        {
            NoRolesMessage.Visible = true;
            EntryContainer.DisposeAllChildren();
        }

        public void AddEntry(string name, string description, IEnumerable<GhostRoleInfo> roles)
        {
            NoRolesMessage.Visible = false;

            var entry = new GhostRolesEntry(name, description, roles);
            entry.OnRoleSelected += OnRoleRequested;
            entry.OnRoleFollow += OnRoleFollow;
            EntryContainer.AddChild(entry);
        }

        public void AddDenied(int denied)
        {
            if (denied == 0)
                return;

            NoRolesMessage.Visible = false;

            var message = Loc.GetString("ghost-role-whitelist-text", ("num", denied));

            if (denied == 1)
                message = Loc.GetString("ghost-role-whitelist-text-one");

            var textLabel = new RichTextLabel();
            textLabel.SetMessage(message);
            EntryContainer.AddChild(textLabel);

            var whitelistButton = new Button();
            whitelistButton.Text = Loc.GetString("ui-escape-discord");

            var uri = IoCManager.Resolve<IUriOpener>();
            var cfg = IoCManager.Resolve<IConfigurationManager>();

            whitelistButton.OnPressed += _ =>
            {
                uri.OpenUri(cfg.GetCVar(CCVars.InfoLinksDiscord));
            };

            EntryContainer.AddChild(whitelistButton);
        }

        public void SetRedirect(bool enabled)
        {
            var textLabel = new RichTextLabel();
            textLabel.SetMessage(Loc.GetString("ghost-roles-window-redirect-label"));
            // EntryContainer.AddChild(textLabel);

            // var redirectButton = new Button();
            // redirectButton.Text = Loc.GetString("ghost-roles-window-redirect");
            // EntryContainer.AddChild(redirectButton);
            // redirectButton.OnPressed += _ => Redirect();
        }

        private void Redirect()
        {
            var red = EntitySystem.Get<RedialSystem>();
            red.TryRedialToRandom();
        }
    }
}
