﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Move_It
{
    class Commands
    {
        private List<Category> categories = Category.Categories();

        [Command("move")]
        [Description("Moves users from voice channel in specified category")]
        public async Task Move(CommandContext ctx, [Description("Number of category to clean. Number can be retrieved from /list")] int type)
        {
            if (ctx.Member.Id == 228248704379912192)
            {
                try
                {
                    await Task.Delay(300);
                    DiscordChannel category = Category.ReturnCategory(ctx, type, categories);
                    await Task.Delay(300);
                    List<DiscordChannel> channels = new List<DiscordChannel>();
                    foreach (DiscordChannel chnl in ctx.Guild.Channels)
                    {
                        try
                        {
                            if (chnl.ParentId == categories[type].id && chnl.Type == ChannelType.Voice)
                                channels.Add(chnl);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    Category.DeleteChannels(ctx, type, categories);
                    Category.CreateChannels(ctx, channels, category);
                    await Task.Delay(300);
                    await ctx.RespondAsync("Successfully removed users from " + categories[type].name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        [Command("list")]
        [Description("Lists all available categories")]
        public async Task List(CommandContext ctx)
        {
            if (ctx.Member.Id == 228248704379912192)
            {
                string list = "````\n";
                int i = 0;
                foreach (Category c in categories)
                {
                    list += String.Format("{0} - {1}\n", i, c.name);
                    i++;
                }
                list += "```";
                await ctx.RespondAsync(list);
            }
        }
        [Command("reload")]
        [Description("Reloads list of categories")]
        public async Task Reload(CommandContext ctx)
        {
            if (ctx.Member.Id == 228248704379912192)
            {
                try
                {
                    categories = Category.Categories();
                    await ctx.RespondAsync("Successfully reloaded list");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }
    }
}
