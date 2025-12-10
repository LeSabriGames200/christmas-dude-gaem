using UnityEngine;

public class LipSyncController : MonoBehaviour
{
    public Sprite[] mouthSprites; // Array of mouth sprites representing different phonemes
    public float animationSpeed = 10f; // Speed at which the lip-sync animation plays
    public float volumeThreshold = 0.1f; // Minimum volume threshold to trigger mouth opening

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0; // Current frame index

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check if there are mouth sprites and audio is playing
        if (mouthSprites != null && audioSource.isPlaying)
        {
            // Calculate the current animation frame based on the volume level
            float volumeLevel = GetVolumeLevel();
            currentFrame = Mathf.FloorToInt(volumeLevel * (mouthSprites.Length - 1));

            // Reset to the first frame if the volume is below the threshold
            if (volumeLevel < volumeThreshold)
            {
                currentFrame = 0;
            }
        }
        else
        {
            // Reset to the first frame when audio is not playing
            currentFrame = 0;
        }

        // Update the sprite renderer with the corresponding mouth sprite
        spriteRenderer.sprite = mouthSprites[currentFrame];
    }

    float GetVolumeLevel()
    {
        // Calculate the average volume level from the audio samples
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);
        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        float average = sum / samples.Length;

        // Map the average volume level to a range between 0 and 1
        float volumeLevel = Mathf.Clamp01(average / volumeThreshold);

        return volumeLevel;
    }
}