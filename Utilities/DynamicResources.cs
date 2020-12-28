using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    internal static class DynamicResources
    {        
        public static Bitmap b_edit_copy = new Bitmap("./themes/" + Setting.language + "/Edit_Copy.png");
        public static Bitmap b_edit_opensprite = new Bitmap("./themes/" + Setting.language + "/Edit_OpenStorage.png");
        public static Bitmap b_edit_reset = new Bitmap("./themes/" + Setting.language + "/Edit_Reset.png");
        public static Bitmap b_full_speak_box = new Bitmap(Properties.Resources.SpeakBox);
        public static Bitmap b_half_speak_box = new Bitmap(b_full_speak_box, b_full_speak_box.Width, (int)(b_full_speak_box.Height * 0.7));

        public static Bitmap b_msg_box_form = new Bitmap("./themes/" + Setting.language + "/MsgBoxForm.png");
        public static Bitmap b_msg_box_ok_btn = new Bitmap("./themes/" + Setting.language + "/MsgBoxOKBtn.png");
        public static Bitmap b_msg_box_no_btn = new Bitmap("./themes/" + Setting.language + "/MsgBoxNoBtn.png");
        public static Bitmap b_msg_box_yes_btn = new Bitmap("./themes/" + Setting.language + "/MsgBoxYesBtn.png");
        public static Bitmap b_msg_box_cancel_btn = new Bitmap("./themes/" + Setting.language + "/MsgBoxCancelBtn.png");        
        public static Bitmap b_recording_form = new Bitmap("./themes/" + Setting.language + "/RecordingForm.png");
        public static Bitmap b_speak_text_form = new Bitmap("./themes/" + Setting.language + "/SpeakTextForm.png");
        
        public static Font font = new Font(FontLibrary.private_font.Families[0], 17F);        
        public static Cursor cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

        public static Dictionary<string, Bitmap> block_images = new Dictionary<string, Bitmap>()
        {
            { "MoveDown1", Properties.Resources.MoveDown1 },
            { "MoveDown5", Properties.Resources.MoveDown5 },
            { "MoveDown10", Properties.Resources.MoveDown10 },
            { "MoveLeft1", Properties.Resources.MoveLeft1 },
            { "MoveLeft5", Properties.Resources.MoveLeft5 },
            { "MoveLeft10", Properties.Resources.MoveLeft10 },
            { "MoveRight1", Properties.Resources.MoveRight1 },
            { "MoveRight5", Properties.Resources.MoveRight5 },
            { "MoveRight10", Properties.Resources.MoveRight10 },
            { "MoveUp1", Properties.Resources.MoveUp1 },
            { "MoveUp5", Properties.Resources.MoveUp5 },
            { "MoveUp10", Properties.Resources.MoveUp10 },
            { "ActionSlow", Properties.Resources.ActionSlow },
            { "ActionFast", Properties.Resources.ActionFast },
            { "ActionFlash", Properties.Resources.ActionFlash },
            { "ActionRRotate", Properties.Resources.ActionRRotate },
            { "ActionLRotate", Properties.Resources.ActionLRotate },
            { "ActionRotateLoop", Properties.Resources.ActionRotateLoop },
            { "ActionWave", Properties.Resources.ActionWave },
            { "ActionTWave", Properties.Resources.ActionTWave },
            { "ActionRandomMove", Properties.Resources.ActionRandomMove },
            { "ActionZigzag", Properties.Resources.ActionZigzag },
            { "ActionTZigzag", Properties.Resources.ActionTZigzag },
            { "ActionBounce", Properties.Resources.ActionBounce },
            { "ActionJump", Properties.Resources.ActionJump },
            { "ActionRLJump", Properties.Resources.ActionRLJump },
            { "ActionAnimate", Properties.Resources.ActionAnimate },
            { "MoveLUpN", Properties.Resources.MoveLUpN },
            { "MoveUpN", Properties.Resources.MoveUpN },
            { "MoveRUpN", Properties.Resources.MoveRUpN },
            { "MoveLeftN", Properties.Resources.MoveLeftN },
            { "MoveEmpty", Properties.Resources.MoveEmpty },
            { "MoveRightN", Properties.Resources.MoveRightN },
            { "MoveLDownN", Properties.Resources.MoveLDownN },
            { "MoveDownN", Properties.Resources.MoveDownN },
            { "MoveRDownN", Properties.Resources.MoveRDownN },
            { "ActionSlowN", Properties.Resources.ActionSlowN },
            { "ActionFastN", Properties.Resources.ActionFastN },
            { "ActionFlashN", Properties.Resources.ActionFlashN },
            { "ActionRRotateN", Properties.Resources.ActionRRotateN },
            { "ActionLRotateN", Properties.Resources.ActionLRotateN },
            { "ActionRotateLoopN", Properties.Resources.ActionRotateLoopN },
            { "ActionWaveN", Properties.Resources.ActionWaveN },
            { "ActionTWaveN", Properties.Resources.ActionTWaveN },
            { "ActionZigzagN", Properties.Resources.ActionZigzagN },
            { "ActionTZigzagN", Properties.Resources.ActionTZigzagN },
            { "ActionJumpN", Properties.Resources.ActionJumpN },
            { "ActionRLJumpN", Properties.Resources.ActionRLJumpN },
            { "ActionAnimateN", Properties.Resources.ActionAnimateN },
            { "ControlTime1", Properties.Resources.ControlTime1 },
            { "ControlTime2", Properties.Resources.ControlTime2 },
            { "ControlTimeN", Properties.Resources.ControlTimeN },
            { "ControlLoopN", Properties.Resources.ControlLoopN },
            { "ControlLoop", Properties.Resources.ControlLoop },
            { "ControlFlag", Properties.Resources.ControlFlag },
            { "ControlFlipX", Properties.Resources.ControlFlipX },
            { "ControlFlipY", Properties.Resources.ControlFlipY },
            { "ControlNextSprite", Properties.Resources.ControlNextSprite },
            { "ControlShow", Properties.Resources.ControlShow },
            { "ControlHide", Properties.Resources.ControlHide },
            { "ControlSound", Properties.Resources.ControlSound },
            { "ControlSpeak", Properties.Resources.ControlSpeak },
            { "ControlSpeakStop", Properties.Resources.ControlSpeakStop },
            { "ControlChangeBack", Properties.Resources.ControlChangeBack },
            { "ControlSendSig", Properties.Resources.ControlSendSig },
            { "ControlSendSigWait", Properties.Resources.ControlSendSigWait },
            { "ControlMoveXY", Properties.Resources.ControlMoveXY },
            { "ControlChangeVal", Properties.Resources.ControlChangeVal },
            { "ControlSetVal", Properties.Resources.ControlSetVal },
            { "ControlStop", Properties.Resources.ControlStop },
            { "ControlClone", Properties.Resources.ControlClone },
            { "ControlCondition", Properties.Resources.ControlCondition },
            { "GameDown", Properties.Resources.GameDown },
            { "GameJump", Properties.Resources.GameJump },
            { "GameUp", Properties.Resources.GameUp },
            { "GameLeft", Properties.Resources.GameLeft },
            { "GameLoopN", Properties.Resources.GameLoopN },
            { "GameRight", Properties.Resources.GameRight },
            { "GameFlag", Properties.Resources.GameFlag },
            { "EventStart", Properties.Resources.EventStart },
            { "EventRecvSig", Properties.Resources.EventRecvSig },
            { "EventInputKey", Properties.Resources.EventInputKey },
            { "EventTouch", Properties.Resources.EventTouch },
            { "EventClickSprite", Properties.Resources.EventClickSprite },
            { "EventClone", Properties.Resources.EventClone },
        };
    }
}