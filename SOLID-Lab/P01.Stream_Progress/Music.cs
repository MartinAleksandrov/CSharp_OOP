namespace P01.Stream_Progress
{
    public class Music : File
    {
        public Music(string artist,string album, int length, int bytesSent) : base(artist, length, bytesSent)
        {
            this.Artist = artist;
            this.Album = album;
        } 
        public string Artist { get; }
        public string Album { get; }
    }
}
