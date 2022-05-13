# InputMvvm
Sample App with MVVM and SQLite

This is a small sample app I made with Xamarin Forms and SQLite.  If you click in the upper left corner in the whitespace ( there is a hamburger icon 
that is hard to see based on the Xamarin default sample app) a Userpage menu item will appear.  The Userpage displays a very simple form that allows 
input of a Username, cat name and a dog name.   The form is validated so that a duplicate username cannot be used and all of the fields are required. 

This app is relatively simple, but incorporates a few common industry standard design patterns.  It uses MVVM for binding between the view and the 
viewmodel.  It uses dependency injection and an interface (data repository) to allow for reuse and access to the data service.  This would allow for 
easily changing databases if needed. 

This was a quick sample effort.  This app falls short in a few areas due to time constraints.  First off the data validation is pretty brute force using MVVM binding to update a few objects in the view.  In a production app I would incorporate a more robust validation scheme such as valadatableobjects. I did not setup unit testing or automated UI testing, but these are also features necessary for a more production ready app.  I could see where a Unit testing project, MVVM framework like Prism and Moq data could be added.
