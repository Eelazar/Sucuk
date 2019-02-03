using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackRegistry {

    public static List<GameObject> percussionOrblings = new List<GameObject>();
    public static List<GameObject> bassOrblings = new List<GameObject>();
    public static List<GameObject> leadOrblings = new List<GameObject>();

    public static float[] destructionTimestamps = new float[] { 15F, 63F, 80F };

    public static int segment;
    
    public static List<string> percussionTracks = new List<string>();
    public static List<string> bassTracks = new List<string>();
    public static List<string> leadTracks = new List<string>();

    public static List<string> percussionTracksIntro = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksIntro = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05", "intro_bass06" };
    public static List<string> leadTracksIntro = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04", "intro_lead05" };

    public static List<string> percussionTracksBreakdown1 = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksBreakdown1 = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05", "intro_bass06" };
    public static List<string> leadTracksBreakdown1 = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04", "intro_lead05" };

    public static List<string> percussionTracksDrop1 = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksDrop1 = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05", "intro_bass06" };
    public static List<string> leadTracksDrop1 = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04", "intro_lead05" };

    public static List<string> percussionTracksBreakdown2 = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksBreakdown2 = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05", "intro_bass06" };
    public static List<string> leadTracksBreakdown2 = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04", "intro_lead05" };

    public static List<string> percussionTracksDrop2 = new List<string> { "intro_perc01", "intro_perc02", "intro_perc03" };
    public static List<string> bassTracksDrop2 = new List<string> { "intro_bass01", "intro_bass02", "intro_bass03", "intro_bass04", "intro_bass05", "intro_bass06" };
    public static List<string> leadTracksDrop2 = new List<string> { "intro_lead01", "intro_lead02", "intro_lead03", "intro_lead04", "intro_lead05" };
}
