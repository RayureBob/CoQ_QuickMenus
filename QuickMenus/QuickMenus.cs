using System;
using System.Collections.Generic;
using XRL.UI;
using System.Linq;
using XRL.World;
using ConsoleLib.Console;
using XRL.World.Anatomy;
using Qud.API;

namespace XRL.World.Parts
{
    public class QuickMenus : IPart
    {
        public const string Command_MissileQuickMenu = "MissileWeaponSwapMenu";
        public const string Command_RecoilersQuickMenu = "RecoilerQuickMenu";

        private const string C_MISSILE_WEAPON = "Missile Weapon";
        private const string B_RECOILER = "BaseRecoiler";

        public override void Register(GameObject Object)
        {
            Object.RegisterPartEvent(this, Command_MissileQuickMenu);
            Object.RegisterPartEvent(this, Command_RecoilersQuickMenu);
            base.Register(Object);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == Command_MissileQuickMenu)
            {
                return MissileWeaponOptionList();
            }

            if(E.ID == Command_RecoilersQuickMenu)
            {
                return RecoilerOptionList();
            }

            return false;
        }

        private bool MissileWeaponOptionList()
        {
             GameObject[] missileWeapons = _ParentObject.Inventory.GetObjects().Where(g => g.GetInventoryCategory() == C_MISSILE_WEAPON).ToArray();

            if (missileWeapons.Length < 1)
            {
                Popup.Show("You have no available missile weapon !");
                return false;
            }

            return DisplayObjectsOptionList(missileWeapons, "Missile Weapons");
        }

        private bool DisplaySelectionBase()
        {
            GameObject[] targetObjects = _ParentObject.Inventory.GetObjects().ToArray();
            string[] options = new string[targetObjects.Length];

            for (int i = 0; i < targetObjects.Length; i++)
            {
                options[i] = targetObjects[i].DisplayName;
            }

            Render[] icons = new Render[targetObjects.Length];

            for (int i = 0; i < targetObjects.Length; i++)
            {
                icons[i] = targetObjects[i]._pRender;
            }

            /*
            Popup.ShowOptionList(
                string Title,
                string[] Options,
                char[] Hotkeys,
                int Spacing,
                string Intro,
                int maxWidth,
                bool SpectectOptionNewLines,
                bool AllowEscape,
                int defaultSeclected,
                string spacingText,
                Action<int> OnResult,
                XRL.World.GameObject context,
                IRenderable[] icons,
                QudMenuItem[] Buttons,
                bool centerIntro,
                int iconPosition,
                bool forceNewPopup
            );
            */


            Popup.ShowOptionList(
                "Poopup list test",
                Options: options,
                onResult: index => AddPlayerMessage(targetObjects[index].GetBlueprint().GetBase()),
                AllowEscape: true,
                Icons: icons
            );

            return true;
        }

        private bool RecoilerOptionList()
        {
            GameObject[] recoilers = _ParentObject.Inventory.GetObjects().Where(g => g.GetBlueprint().GetBase() == B_RECOILER).ToArray();

            if(recoilers.Length < 1)
            {
                Popup.Show("You don't have any recoilers !");
                return false;
            }

            return DisplayObjectsOptionList(recoilers, "Recoilers");
        }

        private bool DisplayObjectsOptionList(GameObject[] targetObjects, string menuName)
        {
            string[] options = new string[targetObjects.Length];

            for (int i = 0; i < targetObjects.Length; i++)
            {
                options[i] = targetObjects[i].DisplayName;
            }

            Render[] icons = new Render[targetObjects.Length];

            for (int i = 0; i < targetObjects.Length; i++)
            {
                icons[i] = targetObjects[i]._pRender;
            }

            /*
            Popup.ShowOptionList(
                string Title,
                string[] Options,
                char[] Hotkeys,
                int Spacing,
                string Intro,
                int maxWidth,
                bool SpectectOptionNewLines,
                bool AllowEscape,
                int defaultSeclected,
                string spacingText,
                Action<int> OnResult,
                XRL.World.GameObject context,
                IRenderable[] icons,
                QudMenuItem[] Buttons,
                bool centerIntro,
                int iconPosition,
                bool forceNewPopup
            );
            */


            Popup.ShowOptionList(
                menuName,
                Options: options,
                onResult: index => EquipmentAPI.TwiddleObject(targetObjects[index]),
                AllowEscape: true,
                Icons: icons
            );

            return true;
        }

        private void DisplayAllParts()
        {
            BodyPart[] parts = ThePlayer.Body.GetParts().ToArray();
            string[] options = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                options[i] = parts[i].Name;
            }

            Popup.ShowOptionList(
                    "Poopup list test",
                    Options: options,
                    onResult: index => AddPlayerMessage(parts[index].Type),
                    AllowEscape: true
                );
        }
    }
}
