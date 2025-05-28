public class AppScreen_GeneralCanvas_VirtualStick_Visual_Outer : AppScreen_GeneralCanvas_VirtualStick_Visual_Parent
{
    private void Start()
    {
        AppScreen_GeneralCanvas_VirtualStick_Entity.SingleOnScene.Visual_Outer = this;
    }
}
