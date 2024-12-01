public class VideoSharingPlatform
{
    private class Video
    {
        public int Id { get; }
        public string Content { get; }
        private int likes;
        private int dislikes;
        private int views;

        public Video(int id, string content)
        {
            Id = id;
            Content = content;
            likes = 0;
            dislikes = 0;
            views = 0;
        }

        public void Like() => likes++;

        public void Dislike() => dislikes++;

        public void IncrementViews() => views++;

        public int GetViews() => views;

        public int[] GetLikesAndDislikes() => new int[] { likes, dislikes };

        public string Watch(int startMinute, int endMinute)
        {
            if (startMinute < 0)
            {
                startMinute = 0;
            }

            if (endMinute > Content.Length - 1)
            {
                endMinute = Content.Length - 1;
            }

            return Content.Substring(startMinute, endMinute - startMinute + 1);
        }
    }

    private readonly Dictionary<int, Video> videos;
    private readonly PriorityQueue<int, int> _ids;
    private int nextId;

    public VideoSharingPlatform()
    {
        videos = [];
        _ids = new PriorityQueue<int, int>();
    }

    private int GetFreeId()
    {
        int id;
        if (_ids.Count == 0)
        {
            id = nextId;
            nextId++;
        }
        else
        {
            id = _ids.Dequeue();
        }
        return id;
    }

    public int Upload(string videoContent)
    {
        if (string.IsNullOrEmpty(videoContent))
            return -1;

        int videoId = GetFreeId();
        videos[videoId] = new Video(videoId, videoContent);
        return videoId;
    }

    public void Remove(int videoId)
    {
        if (videos.Remove(videoId))
        {
            _ids.Enqueue(videoId, videoId);
        }
    }

    public string Watch(int videoId, int startMinute, int endMinute)
    {
        if (videos.TryGetValue(videoId, out Video video))
        {
            video.IncrementViews();
            return video.Watch(startMinute, endMinute);
        }

        return "-1";
    }

    public void Like(int videoId)
    {
        if (videos.TryGetValue(videoId, out Video video))
        {
            video.Like();
        }
    }

    public void Dislike(int videoId)
    {
        if (videos.TryGetValue(videoId, out Video video))
        {
            video.Dislike();
        }
    }

    public int[] GetLikesAndDislikes(int videoId)
    {
        if (videos.TryGetValue(videoId, out Video video))
        {
            return video.GetLikesAndDislikes();
        }

        return [-1];
    }

    public int GetViews(int videoId)
    {
        if (videos.TryGetValue(videoId, out Video video))
        {
            return video.GetViews();
        }

        return -1;
    }
}