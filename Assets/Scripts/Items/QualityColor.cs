using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine;


public enum Quality { Common, Uncommon, Rare, Epic, Mific }
public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        { Quality.Common, "#9B9B9B" },
         { Quality.Uncommon, "#4f8ea5" },
          { Quality.Rare, "#f3c100" },
           { Quality.Epic, "#9248f0" },
            { Quality.Mific, "#ff4b4b" },

    };

    public static Dictionary<Quality, string> MyColors { get => colors; }
}
