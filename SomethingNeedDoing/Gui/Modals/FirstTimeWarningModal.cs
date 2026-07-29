using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.ImGuiMethods;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace SomethingNeedDoing.Gui.Modals;
public static class FirstTimeWarningModal
{
    private static Vector2 Size = new(600, 600);

    public static void Close()
    {
        C.AcknowledgedLegacyWarning = true;
        ImGui.CloseCurrentPopup();
    }

    public static unsafe void DrawModal()
    {
        if (C.AcknowledgedLegacyWarning || !AgentLobby.Instance()->IsLoggedIntoZone) return;
        var isOpen = !C.AcknowledgedLegacyWarning;

        ImGui.OpenPopup($"FirstTimeWarningPopup##{nameof(FirstTimeWarningModal)}");

        ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));
        ImGui.SetNextWindowSize(Size);

        using var style = ImRaii.PushStyle(ImGuiStyleVar.WindowPadding, new Vector2(15, 15));
        using var popup = ImRaii.PopupModal($"FirstTimeWarningPopup##{nameof(FirstTimeWarningModal)}", ref isOpen, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoTitleBar);
        if (!popup) return;

        ImGui.TextWrapped($"{P.Name} 已全面重寫，以支援 API 12 帶來的框架變更。");
        ImGui.BulletText("原生巨集的運作方式應與過去大致相同。");
        ImGui.BulletText("舊版 Lua 巨集無法運作，腳本作者需要重新編寫腳本。");
        ImGui.BulletText("設定選單中提供舊版巨集匯入工具。");

        ImGui.Spacing();
        ImGuiEx.TextCentered(ImGuiColors.DalamudGrey, "此訊息只會顯示一次。");

        var group = new ImGuiEx.EzButtonGroup() { IsCentered = true };
        group.Add("Acknowledge and Close", Close);
        group.Draw();
    }
}
