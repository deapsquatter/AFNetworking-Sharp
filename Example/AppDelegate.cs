using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

using AFNetworking;

namespace Example
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		ExampleViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new ExampleViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			AFHTTPClient client = new AFHTTPClient (new NSUrl ("https://alpha-api.app.net"));
			client.RegisterHTTPOperationClass (new Class (typeof(AFJSONRequestOperation)));
			client.SetDefaultHeader ("Accept", "application/json");
			Console.WriteLine (client.Description);
			client.GetPath ("stream/0/posts/stream/global",
			                null,
			                (request, response) => {
								Console.WriteLine (response.GetType());
								Console.WriteLine (response);
								Console.WriteLine (response.ValueForKey(new NSString("data")));
							},
							(request, error) => {
								Console.WriteLine ("T2");
							});
			
			return true;
		}
	}
}

