using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.ImGuiMethods;
using SomethingNeedDoing.Managers;

namespace SomethingNeedDoing.Gui.Tabs;
public static class HelpGeneralTab
{
    public static void DrawTab()
    {
        using var child = ImRaii.Child(nameof(HelpGeneralTab));
        ImGuiUtils.Section(P.Name, () =>
        {
            ImGui.TextWrapped($"{P.Name} 擴充了原生巨集系統，提供智慧輔助、額外指令與修飾詞，以及不限數量的巨集。");
            ImGui.TextWrapped("同時支援 Lua 腳本，可編寫原生系統無法處理的複雜巨集。");
        });

        ImGuiUtils.Section("狀態監控", () =>
        {
            ImGui.TextWrapped("狀態視窗會顯示所有目前執行中的巨集及其狀態。");
            ImGui.Spacing();

            ImGuiEx.Text(ImGuiColors.DalamudOrange, "巨集狀態");
            ImGui.BulletText("就緒：巨集已載入但尚未開始執行");
            ImGui.BulletText("執行中：巨集目前正在執行");
            ImGui.BulletText("已暫停：巨集執行已暫時停止");
            ImGui.BulletText("已完成：巨集已完成執行");
            ImGui.BulletText("失敗：巨集執行期間發生錯誤");
        });

        ImGuiUtils.Section("觸發事件", () =>
        {
            ImGui.TextWrapped("可設定巨集在特定遊戲事件發生時自動觸發：");
            Enum.GetNames<TriggerEvent>().Each(name => ImGui.BulletText(name));

            ImGui.TextWrapped("Lua 巨集也可設定個別函式自動觸發（腳本必須已在執行）。");
            ImGui.TextWrapped($"以 TriggerEvent 名稱開頭的函式，會在腳本啟動時註冊至 {nameof(TriggerEventManager)}。");
            ImGui.TextWrapped($"{TriggerEvent.OnAddonEvent} 的事件名稱後方必須接續介面名稱與事件類型，例如：");
            ImGui.SameLine();
            ImGuiEx.Text(ImGuiColors.DalamudOrange, "OnAddonEvent_SelectYesno_PostSetup");
        });

        ImGuiUtils.Section("巨集中繼資料", () =>
        {
            ImGuiEx.Text(ImGuiColors.DalamudOrange, "一般");
            ImGui.TextWrapped("巨集可透過中繼資料向框架提供執行相關設定。");
            ImGui.TextWrapped("在巨集庫中選取巨集後，可於巨集設定區段編輯中繼資料。");
            ImGui.TextWrapped("可使用上方按鈕將中繼資料寫入檔案；對存放於 GitHub 等遠端位置的巨集而言，這能讓框架在匯入時得知應使用的設定。");

            ImGuiEx.Text(ImGuiColors.DalamudOrange, "依賴與衝突");
            ImGui.TextWrapped("巨集可設定為需要其他插件才能執行；若未滿足條件，將顯示訊息。");
            ImGui.TextWrapped("也可設定巨集在執行期間停用其他插件，但該插件必須預先在框架中定義。");
            ImGui.TextWrapped("巨集亦可依賴其他本地或遠端巨集。");
        });

        ImGuiUtils.Section("Git 整合", () =>
        {
            ImGui.TextWrapped("巨集可連結 GitHub 網址，並自動轉換為「Git 巨集」。");
            ImGui.TextWrapped("Git 巨集會在啟動時檢查更新，並可透過巨集設定中的版本歷史視窗切換版本。");
            ImGui.TextWrapped("中繼資料含有儲存庫網址時，巨集會自動識別為 Git 巨集。清除網址或點擊設定中的重設 Git 資料，即可轉回本地巨集。");
        });
    }
}
