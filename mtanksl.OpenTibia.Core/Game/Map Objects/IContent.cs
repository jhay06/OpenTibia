﻿namespace OpenTibia
{
    public interface IContent
    {
        TopOrder TopOrder { get; }

        IContainer Container { get; set; }
    }
}