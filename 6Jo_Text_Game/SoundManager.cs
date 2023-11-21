using NAudio.Wave;

class SoundManager
{
    private bool isBackgroundMusicPlaying = false;
    private WaveOutEvent outputDevice;

    public async Task PlayBackgroundMusicAsync(string musicFilename)
    {
        if (isBackgroundMusicPlaying)
        {
            // 이미 배경 음악이 재생 중인 경우 중복 재생 방지
            return;
        }

        isBackgroundMusicPlaying = true;

        try
        {
            // 배경 음악 비동기적으로 재생
            await PlaySoundAsync(musicFilename);
        }
        finally
        {
            isBackgroundMusicPlaying = false;
        }
    }

    private async Task PlaySoundAsync(string filename)
    {
        await Task.Run(() =>
        {
            using (var audioFile = new AudioFileReader($"../../../Sound/{filename}.mp3"))
            {
                outputDevice = new WaveOutEvent();
                outputDevice.Volume = 0.5f;

                outputDevice.Init(audioFile);
                outputDevice.Play();

                // 여기서 원하는 조건에 따라 재생을 멈출 수 있습니다.
                // 아래는 재생 중인 동안 500ms마다 확인하는 예시입니다.
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }

                // 재생이 끝나면 정리
                outputDevice.Dispose();
            }
        });
    }

    public void CallSound(string filename, int time)
    {
        try
        {
            using (var audioFile = new AudioFileReader($"../../../Sound/{filename}.mp3"))
            {
                outputDevice = new WaveOutEvent();
                outputDevice.Volume = 0.5f; // 사운드 볼륨

                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1);
                    if (Console.KeyAvailable)
                    {
                        // 키 입력이 감지되면 사운드 재생 중지
                        outputDevice.Stop();
                        outputDevice.Dispose();
                        Console.ReadKey();
                        break;
                    }
                }

                outputDevice.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오디오 재생 중 오류 발생: {ex.Message}");
        }
    }

    // 다른 부분에서 음악을 중지시킬 때 사용할 메서드
    public void StopMusic()
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
            outputDevice.Dispose();
        }
    }
}
