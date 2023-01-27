# UnityEventSystem
Unity event system library

I use zenject for using event library but you can use it in every way you want.
For eventId you can create static class with const itn variables or create enum for that.
Each event need a class or struct for send and recieve.
Don't forget to remove listening with RemoveEvent() func from objects that you made them disable or dewstroy otherwise 
They will listen and may cause some errors!
Happy using
