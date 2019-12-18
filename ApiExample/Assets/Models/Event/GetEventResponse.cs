using System;
using UnityEngine.Serialization;


[Serializable]
public class GetEventResponse
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int id;

    /// <summary>
    /// Gets or sets the time to start.
    /// </summary>
    /// <value>
    /// The timeID to start.
    /// </value>
   public int start;

    /// <summary>
    /// Gets or sets the time to finish.
    /// </summary>
    /// <value>
    /// The time to finish.
    /// </value>
    public int finish;

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    public string title;
}