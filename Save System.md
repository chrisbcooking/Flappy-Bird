Our goal is to save a integer on our system and load it later as our high score.
* Define a Save path string on our file system using PersistentDataPath
* Create a save method in our class. Then, we provide the path and mode of which to access the file in the parameters(Read, Write, Create).
* Encapsulating the File.Open lib, we create a new instance of the BinaryWriter class to choose a format of how we want to write the data. In our case, we'll use the raw binary data (Obvious given the name of the class).
* Now, when opening a file stream and choosing to write data into it with a formatter class/lib, we need to ensure that if any exceptions raise, we clean up the resources we are using, otherwise it could corrupt the file, or cause resource leakage. To do this, we use the "Using" method, this ensures that no matter the file it will be disposed of after finishing the task or raising an exception.
* Next we'll use the write method of the BinaryWriter object to save our data or score to the file we created.
* For loading, we'll do the same as we did for the writer, but use the BinaryReader class instead while using the .Open mode instead of .Create.
* Since we are saving integers which are 4 bytes or 32 bits, we'll use the .ReadInt32 method of the BinaryReader object.
* Finally, we'll apply the deserialized integer to our highscore UI 
  