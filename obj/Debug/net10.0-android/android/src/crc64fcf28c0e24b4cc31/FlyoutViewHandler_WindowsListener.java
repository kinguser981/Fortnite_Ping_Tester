package crc64fcf28c0e24b4cc31;


public class FlyoutViewHandler_WindowsListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.core.view.OnApplyWindowInsetsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onApplyWindowInsets:(Landroid/view/View;Landroidx/core/view/WindowInsetsCompat;)Landroidx/core/view/WindowInsetsCompat;:GetOnApplyWindowInsets_Landroid_view_View_Landroidx_core_view_WindowInsetsCompat_Handler:AndroidX.Core.View.IOnApplyWindowInsetsListenerInvoker, Xamarin.AndroidX.Core\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Maui.Handlers.FlyoutViewHandler+WindowsListener, Microsoft.Maui", FlyoutViewHandler_WindowsListener.class, __md_methods);
	}

	public FlyoutViewHandler_WindowsListener ()
	{
		super ();
		if (getClass () == FlyoutViewHandler_WindowsListener.class) {
			mono.android.TypeManager.Activate ("Microsoft.Maui.Handlers.FlyoutViewHandler+WindowsListener, Microsoft.Maui", "", this, new java.lang.Object[] {  });
		}
	}

	public androidx.core.view.WindowInsetsCompat onApplyWindowInsets (android.view.View p0, androidx.core.view.WindowInsetsCompat p1)
	{
		return n_onApplyWindowInsets (p0, p1);
	}

	private native androidx.core.view.WindowInsetsCompat n_onApplyWindowInsets (android.view.View p0, androidx.core.view.WindowInsetsCompat p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
