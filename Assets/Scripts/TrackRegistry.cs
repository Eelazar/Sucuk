using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackRegistry {

    public static List<GameObject> percussionOrblings = new List<GameObject>();
    public static List<GameObject> bassOrblings = new List<GameObject>();
    public static List<GameObject> leadOrblings = new List<GameObject>();
    public static List<GameObject> kickOrblings = new List<GameObject>();
    public static List<GameObject> chordOrblings = new List<GameObject>();

    public static float[] destructionTimestamps = new float[] { 135F, 272F, 434F };
    public static float[] spawnTimestamps = new float[] { 20F, 189.5F, 313,85F};

    public static int segment;
    
    public static List<string> percussionTracks = new List<string>();
    public static List<string> bassTracks = new List<string>();
    public static List<string> leadTracks = new List<string>();
    public static List<string> kickTracks = new List<string>();
    public static List<string> chordTracks = new List<string>();


    public static List<string> percussionTracksIntro = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksIntro = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05"};
    public static List<string> leadTracksIntro = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04"};
    public static List<string> kickTracksIntro = new List<string> { "I_kick01", "I_kick02"};
    public static List<string> chordTracksIntro = new List<string> { "intro_chords01", "intro_chords02", "intro_chords03", "intro_chords04" };

    public static List<string> percussionTracksDrop1 = new List<string> { "d1_perc01", "d1_perc02"};
    public static List<string> bassTracksDrop1 = new List<string> { "d1_bass01", "d1_bass02", "d1_bass03", "d1_bass04"};
    public static List<string> leadTracksDrop1 = new List<string> { "d1_lead01", "d1_lead02", "d1_lead03", "d1_lead04" };
    public static List<string> kickTracksDrop = new List<string> { "d1_kick01", "d1_kick02"};
    public static List<string> chordTracksDrop = new List<string> { "d1_chords01", "d2_chords02"};

    public static List<string> percussionTracksDrop2 = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksDrop2 = new List<string> { "d2_bass01", "d2_bass02", "d2_bass03", "d2_bass04" };
    public static List<string> leadTracksDrop2 = new List<string> { "d2_lead01", "d2_lead03", "d2_lead04", "d2_lead05" };
    public static List<string> kickTracksDrop2 = new List<string> { "d2_kick01", "d2_kick02" };
    public static List<string> chordTracksDrop2 = new List<string> { "d2_chords01", "d2_chords02", "d2_chords03", "d2_chords04" };
}
