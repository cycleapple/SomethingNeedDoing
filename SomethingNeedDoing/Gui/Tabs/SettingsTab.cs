using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.ImGuiMethods;
using SomethingNeedDoing.Gui.Modals;

namespace SomethingNeedDoing.Gui.Tabs;
public static class SettingsTab
{
    public static void DrawTab()
    {
        using var _ = ImRaii.Child("SettingsTab", Vector2.Create(-1), false);

        ImGuiUtils.Section("一般設定", () =>
        {
            var chatChannel = C.ChatType;
            if (ImGuiEx.EnumCombo("一般訊息頻道##ChatType", ref chatChannel))
            {
                C.ChatType = chatChannel;
                C.Save();
            }

            var errorChannel = C.ErrorChatType;
            if (ImGuiEx.EnumCombo("錯誤訊息頻道##ErrorChatType", ref errorChannel))
            {
                C.ErrorChatType = errorChannel;
                C.Save();
            }

            var propagatePause = C.PropagateControlsToChildren;
            if (ImGui.Checkbox("將控制操作套用至子巨集##Propagate Controls to Child Macros", ref propagatePause))
            {
                C.PropagateControlsToChildren = propagatePause;
                C.Save();
            }
            ImGuiEx.Tooltip("啟用後，暫停、繼續或停止巨集時，也會對子巨集執行相同操作。");

            var autoOpenStatusWindow = C.AutoOpenStatusWindow;
            if (ImGui.Checkbox("腳本開始時自動開啟狀態視窗##Auto-open Status Window on Script Start", ref autoOpenStatusWindow))
            {
                C.AutoOpenStatusWindow = autoOpenStatusWindow;
                C.Save();
            }
            ImGuiEx.Tooltip("啟用後，腳本開始執行時會自動開啟巨集狀態視窗。");
        });

        ImGuiUtils.Section("生產設定", () =>
        {
            var craftSkip = C.CraftSkip;
            if (ImGui.Checkbox("未進行生產時略過生產技能##Skip craft actions when not crafting", ref craftSkip))
            {
                C.CraftSkip = craftSkip;
                C.Save();
            }

            var smartWait = C.SmartWait;
            if (ImGui.Checkbox("智慧等待生產技能##Smart wait for crafting actions", ref smartWait))
            {
                C.SmartWait = smartWait;
                C.Save();
            }

            var qualitySkip = C.QualitySkip;
            if (ImGui.Checkbox("高品質率達 100% 時略過提升品質技能##Skip quality actions", ref qualitySkip))
            {
                C.QualitySkip = qualitySkip;
                C.Save();
            }

            var loopTotal = C.LoopTotal;
            if (ImGui.Checkbox("將 /loop 數值視為總執行次數##Count loop number as total iterations", ref loopTotal))
            {
                C.LoopTotal = loopTotal;
                C.Save();
            }

            var loopEcho = C.LoopEcho;
            if (ImGui.Checkbox("一律顯示 /loop 指令##Always echo loop commands", ref loopEcho))
            {
                C.LoopEcho = loopEcho;
                C.Save();
            }

            var useCraftLoopTemplate = C.UseCraftLoopTemplate;
            if (ImGui.Checkbox("使用 CraftLoop 範本##Use CraftLoop template", ref useCraftLoopTemplate))
            {
                C.UseCraftLoopTemplate = useCraftLoopTemplate;
                C.Save();
            }

            if (useCraftLoopTemplate)
            {
                var craftLoopTemplate = C.CraftLoopTemplate;
                if (ImGui.InputTextMultiline("CraftLoop 範本##CraftLoop Template", ref craftLoopTemplate, 1000, new Vector2(0, 100)))
                {
                    C.CraftLoopTemplate = craftLoopTemplate;
                    C.Save();
                }

                var craftLoopFromRecipeNote = C.CraftLoopFromRecipeNote;
                if (ImGui.Checkbox("從製作筆記視窗開始生產循環##Start crafting loops from recipe note window", ref craftLoopFromRecipeNote))
                {
                    C.CraftLoopFromRecipeNote = craftLoopFromRecipeNote;
                    C.Save();
                }

                var craftLoopMaxWait = C.CraftLoopMaxWait;
                if (ImGui.SliderInt("CraftLoop 最大等待值##CraftLoop maxwait value", ref craftLoopMaxWait, 1, 10))
                {
                    C.CraftLoopMaxWait = craftLoopMaxWait;
                    C.Save();
                }

                var craftLoopEcho = C.CraftLoopEcho;
                if (ImGui.Checkbox("顯示 CraftLoop 訊息##CraftLoop echo", ref craftLoopEcho))
                {
                    C.CraftLoopEcho = craftLoopEcho;
                    C.Save();
                }
            }
        });

        ImGuiUtils.Section("錯誤處理", () =>
        {
            var stopOnError = C.StopOnError;
            if (ImGui.Checkbox("發生錯誤時停止##Stop on error", ref stopOnError))
            {
                C.StopOnError = stopOnError;
                C.Save();
            }
            ImGuiEx.Tooltip("僅適用於原生巨集。");

            var maxTimeoutRetries = C.MaxTimeoutRetries;
            if (ImGui.SliderInt("逾時最大重試次數##Max Timeout Retries", ref maxTimeoutRetries, 0, 10))
            {
                C.MaxTimeoutRetries = maxTimeoutRetries;
                C.Save();
            }

            var noisyErrors = C.NoisyErrors;
            if (ImGui.Checkbox("錯誤提示音##Noisy Errors", ref noisyErrors))
            {
                C.NoisyErrors = noisyErrors;
                C.Save();
            }

            if (noisyErrors)
            {
                var beepFrequency = C.BeepFrequency;
                if (ImGui.SliderInt("提示音頻率##Beep Frequency", ref beepFrequency, 0, 1000))
                {
                    C.BeepFrequency = beepFrequency;
                    C.Save();
                }

                var beepDuration = C.BeepDuration;
                if (ImGui.SliderInt("提示音長度##Beep Duration", ref beepDuration, 0, 1000))
                {
                    C.BeepDuration = beepDuration;
                    C.Save();
                }

                var beepCount = C.BeepCount;
                if (ImGui.SliderInt("提示音次數##Beep Count", ref beepCount, 0, 10))
                {
                    C.BeepCount = beepCount;
                    C.Save();
                }
            }
        });

        ImGuiUtils.Section("Lua 選項", () =>
        {
            ImGui.TextWrapped("Lua require 路徑（搜尋 Lua 模組的位置）：");

            var paths = C.LuaRequirePaths.ToArray();
            using (ImRaii.Table("LuaRequirePaths", 2, ImGuiTableFlags.SizingStretchProp))
            {
                for (var index = 0; index < paths.Length; index++)
                {
                    var path = PathHelper.NormalizePath(paths[index]);

                    ImGui.TableNextRow();
                    ImGui.TableNextColumn();

                    var isValid = PathHelper.ValidatePath(path);
                    ImGui.TextColored(isValid ? ImGuiColors.HealerGreen : ImGuiColors.DalamudRed, $"路徑 #{index}");
                    ImGuiEx.Tooltip(isValid ? "此路徑有效。" : "此路徑無效。");

                    ImGui.TableNextColumn();

                    if (ImGui.InputText($"##Path{index}", ref path, 200))
                    {
                        var newPaths = paths.ToList();
                        newPaths[index] = PathHelper.NormalizePath(path);
                        C.LuaRequirePaths = [.. newPaths.Where(p => !string.IsNullOrWhiteSpace(p))];
                        C.Save();
                    }
                }
            }

            if (ImGui.Button("新增路徑##Add Path"))
            {
                var newPaths = paths.ToList();
                newPaths.Add(string.Empty);
                C.LuaRequirePaths = [.. newPaths];
                C.Save();
            }
        });

        ImGuiUtils.Section("舊版巨集匯入", () =>
        {
            ImGui.TextWrapped($"從舊版 {P.Name} 匯入巨集。這些巨集不保證仍可運作，但可匯入作為參考。\n" +
            "你可以將舊設定複製到剪貼簿後點擊匯入，或讓插件自動尋找舊設定檔。");
            ImGui.Spacing();

            if (ImGuiUtils.IconButton(FontAwesomeHelper.IconImport, "匯入##Import"))
                MigrationModal.Open(ImGui.GetClipboardText());
        });
    }
}
