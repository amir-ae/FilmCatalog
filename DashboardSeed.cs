namespace FilmCatalog
{
    public static class DashboardSeed
    {
        public static void SeedUserStoreForDashboard(this IApplicationBuilder app)
        {
            SeedStore(app).GetAwaiter().GetResult();
        }

        private async static Task SeedStore(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                IConfiguration? config =
                    scope.ServiceProvider.GetService<IConfiguration>();

                UserManager<IdentityUser>? userManager =
                    scope.ServiceProvider.GetService<UserManager<IdentityUser>>();

                RoleManager<IdentityRole>? roleManager =
                    scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                ApplicationDbContext context = 
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                string roleName = config?["Dashboard:Role"] ?? "Dashboard";
                string userName = config?["Dashboard:User"] ?? "admin@filmcatalog.ru";
                string password = config?["Dashboard:Password"] ?? "mysecret";

                if (!context.Database.CanConnect()
                    || context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (userManager != null && roleManager != null)
                {

                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                    IdentityUser? dashboardUser =
                        await userManager.FindByEmailAsync(userName);

                    if (dashboardUser == null)
                    {
                        dashboardUser = new IdentityUser
                        {
                            UserName = userName,
                            Email = userName,
                            EmailConfirmed = true
                        };
                        await userManager.CreateAsync(dashboardUser);
                        dashboardUser = await userManager.FindByEmailAsync(userName);
                        await userManager.AddPasswordAsync(dashboardUser, password);
                    }

                    if (!await userManager.IsInRoleAsync(dashboardUser, roleName))
                    {
                        await userManager.AddToRoleAsync(dashboardUser, roleName);
                    }
                }

                if (userManager != null && (context.Films == null || !context.Films.Any()))
                {
                    string adminId = (await userManager.FindByNameAsync(userName)).Id;

                    context.Films?.AddRange(
                        new Film
                        {
                            Title = "S’en Fout La Mort",
                            Description = "Two friends (Isaach De Bankolé, Alex Descas) train birds for illegal cock fights, but one has trouble handling the violence inflicted on the animals.",
                            Director = "Claire Denis",
                            Year = 1990,
                            Genre = "Drama",
                            Poster = "No_Fear_No_Die.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Reversal of Fortune",
                            Description = "When socialite Sunny von Bülow (Glenn Close) inexplicably slips into an irreversible coma, police suspect foul play -- and the obvious suspect is her urbane husband, Claus (Jeremy Irons). After being found guilty of murder, Claus is granted a retrial and hires showboat Harvard Law professor Alan Dershowitz (Ron Silver) to represent him. Though unconvinced of Claus's innocence, Dershowitz enjoys a challenge and -- along with a group of his students -- fights to have the verdict overturned.",
                            Director = "Barbet Schroeder",
                            Year = 1990,
                            Genre = "Drama",
                            Poster = "Reversal_of_fortune_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Goodfellas",
                            Description = "A young man grows up in the mob and works very hard to advance himself through the ranks. He enjoys his life of money and luxury, but is oblivious to the horror that he causes. A drug addiction and a few mistakes ultimately unravel his climb to the top. Based on the book 'Wiseguy' by Nicholas Pileggi.",
                            Director = "Martin Scorsese",
                            Year = 1990,
                            Genre = "Drama, Crime",
                            Poster = "Goodfellas.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Jacob’s Ladder",
                            Description = "After returning home from the Vietnam War, veteran Jacob Singer (Tim Robbins) struggles to maintain his sanity. Plagued by hallucinations and flashbacks, Singer rapidly falls apart as the world and people around him morph and twist into disturbing images. His girlfriend, Jezzie (Elizabeth Peña), and ex-wife, Sarah (Patricia Kalember), try to help, but to little avail. Even Singer's chiropractor friend, Louis (Danny Aiello), fails to reach him as he descends into madness.",
                            Director = "Adrian Lyne",
                            Year = 1990,
                            Genre = "Drama, Horror, Mystery & Thriller",
                            Poster = "Jacobsladderposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "King of New York",
                            Description = "A crime lord plots to take control of New York's underground drug economy and distribute the profits to the poor.",
                            Director = "Abel Ferrara",
                            Year = 1990,
                            Genre = "Drama, Crime",
                            Poster = "King_of_new_york_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Dances with Wolves",
                            Description = "A Civil War soldier develops a relationship with a band of Lakota Indians. Attracted by the simplicity of their lifestyle, he chooses to leave his former life behind to be with them. Having observed him, they give the name Dances With Wolves. Soon he is a welcomed member of the tribe and falls in love with a white woman who has been raised in the tribe. Tragedy results when Union soldiers arrive with designs on the land.",
                            Director = "Kevin Costner",
                            Year = 1990,
                            Genre = "Drama, History, Western",
                            Poster = "Dances_with_Wolves_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hitlerjunge Salomon",
                            Description = "Jewish teenager Salek (Marco Hofschneider) is separated from his family when they flee their home in Germany after Kristallnacht. He ends up in a Russian orphanage for two years, but when Nazi troops reach Russia he convinces them he is a German Aryan, and becomes an invaluable interpreter and then an unwitting war hero. His deception becomes increasingly difficult to maintain after he joins the Hitler Youth and finds love with beautiful Leni (Julie Delpy), a fervent anti-Semite.",
                            Director = "Agnieszka Holland",
                            Year = 1990,
                            Genre = "Drama, War, History",
                            Poster = "Europa_Europa_french_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Pretty Woman",
                            Description = "In this modern update on Cinderella, a prostitute and a wealthy businessman fall hard for one another, forming an unlikely pair. While on a business trip in L.A., Edward (Richard Gere), who makes a living buying and breaking up companies, picks up a hooker, Vivian (Julia Roberts), on a lark. After Edward hires Vivian to stay with him for the weekend, the two get closer, only to discover there are significant hurdles to overcome as they try to bridge the gap between their very different worlds.",
                            Director = "Garry Marshall",
                            Year = 1990,
                            Genre = "Romance",
                            Poster = "Pretty_woman_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Archangel",
                            Description = "Lt. John Boles (Kyle McCulloch), a British soldier fighting in World War I, is trapped in the icy Russian town of Archangel. When Boles meets a local couple named Veronkha (Kathy Marykuca) and Philbin (Ari Cohen), he falls for Veronkha because she seems to be his late wife's doppleganger. What follows is a twisted love triangle, as Philbin -- an amnesiac -- forgets that he is married to Veronkha, while Boles begins to think that Veronkha is his dead wife.",
                            Director = "Guy Maddin",
                            Year = 1990,
                            Genre = "Horror",
                            Poster = "Archangelvhs.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Trust",
                            Description = "High school dropout Maria Coughlin (Adrienne Shelly) is having a rough time of it on Long Island. Her father recently died of a heart attack, her boyfriend has left her and she's pregnant. To make matters even worse, her mother has now kicked her out of the house. But when electronics genius Matthew Slaughter (Martin Donovan) comes into her life, things start to brighten up for Maria. Sure, he's unemployed and a little unhinged, but together they just might have a chance.",
                            Director = "Hal Hartley",
                            Year = 1990,
                            Genre = "Romance",
                            Poster = "Trust-film-poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Nema-Ye Nazdik",
                            Description = "A printer's assistant tricks a family into believing that he is famous Iranian filmmaker Mohsen Makhmalbaf.",
                            Director = "Abbas Kiarostami",
                            Year = 1990,
                            Genre = "Drama",
                            Poster = "Close_Up_DVD_cover.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Edward Scissorhands",
                            Description = "A scientist (Vincent Price) builds an animated human being -- the gentle Edward (Johnny Depp). The scientist dies before he can finish assembling Edward, though, leaving the young man with a freakish appearance accentuated by the scissor blades he has instead of hands. Loving suburban saleswoman Peg (Dianne Wiest) discovers Edward and takes him home, where he falls for Peg's teen daughter (Winona Ryder). However, despite his kindness and artistic talent, Edward's hands make him an outcast.",
                            Director = "Tim Burton",
                            Year = 1990,
                            Genre = "Romance, Drama, Fantasy, Comedy",
                            Poster = "Edwardscissorhandsposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Henry: Portrait of a Serial Killer",
                            Description = "Henry (Michael Rooker) is released from prison following his mother's murder. He supplements his job as an exterminator with a series of indiscriminate and violent murders. Fellow jailbird and drug dealer Otis (Tom Towles) becomes a willing accomplice in Henry's bloody killings. But as the depravity escalates and Henry forms a bond with Otis' sister, Becky (Tracy Arnold), things start to get out of hand. The film is based on the true-life story of serial killer Henry Lee Lucas.",
                            Director = "John McNaughton",
                            Year = 1990,
                            Genre = "Drama, Crime",
                            Poster = "Henryportrait.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Total Recall",
                            Description = "Douglas Quaid (Arnold Schwarzenegger) is a bored construction worker in the year 2084 who dreams of visiting the colonized Mars. He visits 'Rekall,' a company that plants false memories into people's brains, in order to experience the thrill of Mars without having to travel there. But something goes wrong during the procedure; Quaid discovers that his entire life is actually a false memory and that the people who implanted it in his head now want him dead.",
                            Director = "Paul Verhoeven",
                            Year = 1990,
                            Genre = "Sci-Fi, Action",
                            Poster = "Total_recall.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Wong Fei-Hung",
                            Description = "A man (Jet Li) must protect his martial-arts school while sorting out his feelings for a young woman (Biao Yuen) who is his aunt by adoption.",
                            Director = "Hark Tsui",
                            Year = 1991,
                            Genre = "Action, Sports & Fitness",
                            Poster = "Onceuponatimeinchina.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Boyz ‘n The Hood",
                            Description = "Tre (Cuba Gooding Jr.) is sent to live with his father, Furious Styles (Larry Fishburne), in tough South Central Los Angeles. Although his hard-nosed father instills proper values and respect in him, and his devout girlfriend Brandi (Nia Long) teaches him about faith, Tre's friends Doughboy (Ice Cube) and Ricky (Morris Chestnut) don't have the same kind of support and are drawn into the neighborhood's booming drug and gang culture, with increasingly tragic results.",
                            Director = "John Singleton",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "Boyz_n_the_hood_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Da Hong Deng Long Gao Gao Gua",
                            Description = "Teenage Songlian (Gong Li), whose family has been devastated by the recent death of her father, becomes the third concubine of wealthy Master Chen (Ma Jingwu). She soon discovers that behind the palatial luxury of life in the master's house, she and her fellow concubines, Zhuoyan (Cao Cuifeng) and Meishan (He Caifei), are pitted against each other in a struggle for his affections. The situation inevitably leads to deception, jealous rages and the revelation of each other's darkest secrets.",
                            Director = "Yimou Zhang",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "Raise_the_Red_Lantern_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Delicatessen",
                            Description = "Clapet (Jean-Claude Dreyfus) is a butcher who owns a run-down apartment building in post-apocalyptic France. The building is in constant need of a handyman, because Clapet routinely butchers them and sells them as food. The latest in the long ling of disposable workers is Louison (Dominique Pinon), a former circus clown desperate for work and lodging. But this time Clapet's plan hits a snag when his young daughter (Marie-Laure Dougnac) falls head over heels for the lovable Louison.",
                            Director = "Jean-Pierre Jeunet, Marc Caro",
                            Year = 1991,
                            Genre = "Sci-Fi",
                            Poster = "Delicatessen2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Guling Jie Shaonian Sha Ren Shijian",
                            Description = "A boy experiences first love, friendships and injustices growing up in 1960s Taiwan.",
                            Director = "Edward Yang",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "A_Brighter_Summer_Day_(movie_poster).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Naked Lunch",
                            Description = "Blank-faced bug killer Bill Lee (Peter Weller) and his dead-eyed wife, Joan (Judy Davis), like to get high on Bill's pest poisons while lounging with Beat poet pals. After meeting the devilish Dr. Benway (Roy Scheider), Bill gets a drug made from a centipede. Upon indulging, he accidentally kills Joan, takes orders from his typewriter-turned-cockroach, ends up in a constantly mutating Mediterranean city and learns that his hip friends have published his work -- which he doesn't remember writing.",
                            Director = "David Cronenberg",
                            Year = 1991,
                            Genre = "Sci-Fi",
                            Poster = "Naked_Lunch_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "La Belle Noiseuse",
                            Description = "A young model (Jane Birkin) replacing his wife (Emmanuelle Béart) inspires a tired painter (Michel Piccoli) to pick up a work he quit 10 years before.",
                            Director = "Jacques Rivette",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "Belle_noiseuse.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Rapture",
                            Description = "Sharon (Mimi Rogers) is a telephone operator who escapes the drudgery of her everyday life by trolling bars with her lover, Vic (Patrick Bauchau), and looking for couples to swing with. However, after having an epiphany while in bed with a stranger, she becomes a born-again Christian and soon joins a sect that believes Judgment Day is near. Sharon settles down, becoming a dedicated wife and mother, but her unflinching faith eventually leads to shocking behavior.",
                            Director = "Michael Tolkin",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "Raptureposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "My Own Private Idaho",
                            Description = "In this loose adaptation of Shakespeare's 'Henry IV,' Mike Waters (River Phoenix) is a gay hustler afflicted with narcolepsy. Scott Favor (Keanu Reeves) is the rebellious son of a mayor. Together, the two travel from Portland, Oregon to Idaho and finally to the coast of Italy in a quest to find Mike's estranged mother. Along the way they turn tricks for money and drugs, eventually attracting the attention of a wealthy benefactor and sexual deviant.",
                            Director = "Gus Van Sant",
                            Year = 1991,
                            Genre = "Drama",
                            Poster = "My_Own_Private_Idaho.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Thelma & Louise",
                            Description = "Meek housewife Thelma (Geena Davis) joins her friend Louise (Susan Sarandon), an independent waitress, on a short fishing trip. However, their trip becomes a flight from the law when Louise shoots and kills a man who tries to rape Thelma at a bar. Louise decides to flee to Mexico, and Thelma joins her. On the way, Thelma falls for sexy young thief J.D. (Brad Pitt) and the sympathetic Detective Slocumb (Harvey Keitel) tries to convince the two women to surrender before their fates are sealed.",
                            Director = "Ridley Scott",
                            Year = 1991,
                            Genre = "Adventure, Drama, Comedy",
                            Poster = "Thelma_&_Louise_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Terminator 2: Judgment Day",
                            Description = "In this sequel set eleven years after 'The Terminator,' young John Connor (Edward Furlong), the key to civilization's victory over a future robot uprising, is the target of the shape-shifting T-1000 (Robert Patrick), a Terminator sent from the future to kill him. Another Terminator, the revamped T-800 (Arnold Schwarzenegger), has been sent back to protect the boy. As John and his mother (Linda Hamilton) go on the run with the T-800, the boy forms an unexpected bond with the robot.",
                            Director = "James Cameron",
                            Year = 1991,
                            Genre = "Sci-Fi, Action",
                            Poster = "Terminator2poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Silence of The Lambs",
                            Description = "Jodie Foster stars as Clarice Starling, a top student at the FBI's training academy. Jack Crawford (Scott Glenn) wants Clarice to interview Dr. Hannibal Lecter (Anthony Hopkins), a brilliant psychiatrist who is also a violent psychopath, serving life behind bars for various acts of murder and cannibalism. Crawford believes that Lecter may have insight into a case and that Starling, as an attractive young woman, may be just the bait to draw him out.",
                            Director = "Jonathan Demme",
                            Year = 1991,
                            Genre = "Drama, Crime, Mystery & Thriller",
                            Poster = "The_Silence_of_the_Lambs_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "JFK",
                            Description = "This acclaimed Oliver Stone drama presents the investigation into the assassination of President John F. Kennedy led by New Orleans district attorney Jim Garrison (Kevin Costner). When Garrison begins to doubt conventional thinking on the murder, he faces government resistance, and, after the killing of suspected assassin Lee Harvey Oswald (Gary Oldman), he closes the case. Later, however, Garrison reopens the investigation, finding evidence of an extensive conspiracy behind Kennedy's death.",
                            Director = "Oliver Stone",
                            Year = 1991,
                            Genre = "Drama, History",
                            Poster = "JFK-poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },

                        new Film
                        {
                            Title = "Slacker",
                            Description = "Austin, Texas, is an Eden for the young and unambitious, from the enthusiastically eccentric to the dangerously apathetic. Here, the nobly lazy can eschew responsibility in favor of nursing their esoteric obsessions. The locals include a backseat philosopher (Richard Linklater) who passionately expounds on his dream theories to a seemingly comatose cabbie (Rudy Basquez), a young woman who tries to hawk Madonna's Pap test to anyone who will listen and a kindly old anarchist looking for recruits.",
                            Director = "Richard Linklater",
                            Year = 1991,
                            Genre = "Comedy",
                            Poster = "Slackerposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Tongues Untied",
                            Description = "Marlon Riggs, with assistance from other gay Black men, especially poet Essex Hemphill, celebrates Black men loving Black men as a revolutionary act. The film intercuts footage of Hemphill reciting his poetry, Riggs telling the story of his growing up, scenes of men in social intercourse and dance, and various comic riffs, including a visit to the 'Institute of Snap!thology,' where men take lessons in how to snap their fingers: the sling snap, the point snap, the diva snap. The film closes with obituaries for victims of AIDS and archival footage of the civil rights movement placed next to footage of Black men marching in a gay pride parade.",
                            Director = "Marlon Riggs",
                            Year = 1991,
                            Genre = "Documentary",
                            Poster = "Tongues-untied.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hearts of Darkness: a Filmmaker’s Apocalypse",
                            Description = "In the late 1970s, as renegade filmmaker Francis Ford Coppola struggles to complete an epic allegory of the Vietnam War, 'Apocalypse Now,' his wife, Eleanor, films his daily travails with a camera of her own. The documentary based on her footage details the difficulties of the large production -- from weather-related delays in the Philippines to star Martin Sheen's heart attack while filming -- and it provides unprecedented behind-the-scenes clips of one of Hollywood's most-acclaimed films.",
                            Director = "Fax Bahr, Eleanor Coppola, George Hickenlooper",
                            Year = 1991,
                            Genre = "Documentary",
                            Poster = "Hearts_of_Darkness,_A_Filmmaker's_Apocalypse_Poster.jpeg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "La Double Vie De Véronique",
                            Description = "Veronique (Irène Jacob) is a beautiful young French woman who aspires to be a renowned singer; Weronika (also Jacob) lives in Poland, has a similar career goal and looks identical to Veronique, though the two are not related. The film follows both women as they contend with the ups and downs of their individual lives, with Veronique embarking on an unusual romance with Alexandre Fabbri (Philippe Volter), a puppeteer who may be able to help her with her existential issues.",
                            Director = "Krzysztof Kieslowski",
                            Year = 1991,
                            Genre = "Mystery & Thriller",
                            Poster = "The_Double_Life_of_Véronique.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Strictly Ballroom",
                            Description = "A top ballroom dancer pairs with a plain, left-footed local girl when his maverick style earns him the disdain of his more conventionally-minded colleagues. Together, the team gives it their all and makes dreams of the National Championship title come true.",
                            Director = "Baz Luhrmann",
                            Year = 1992,
                            Genre = "Musical, Comedy",
                            Poster = "Strictly-ballroom-movie-poster-1992-australian.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Player",
                            Description = "Certain that the anonymous threats he's been receiving are the work of David Kahane (Vincent D'Onofrio), producer Griffin Mill (Tim Robbins) tries to fix things over cocktails. Instead, Griffin ends up murdering the screenwriter and courting the dead man's girlfriend (Greta Scacchi). As police investigate, Griffin concentrates on a prestigious film that might reinvigorate his career. But he soon learns that David's demise hasn't been forgotten by everyone in Hollywood.",
                            Director = "Robert Altman",
                            Year = 1992,
                            Genre = "Comedy",
                            Poster = "Player_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Reservoir Dogs",
                            Description = "A group of thieves assemble to pull of the perfect diamond heist. It turns into a bloody ambush when one of the men turns out to be a police informer. As the group begins to question each other's guilt, the heightening tensions threaten to explode the situation before the police step in.",
                            Director = "Quentin Tarantino",
                            Year = 1992,
                            Genre = "Drama, Crime",
                            Poster = "Reservoir_Dogs.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Romper Stomper",
                            Description = "Hando (Russell Crowe) and Davey (Daniel Pollock) are the leaders of a racist youth gang who spend their nights attacking Asian immigrants in a rough section of Melbourne. On the run after losing badly in a fight against the new Vietnamese owners of their local pub, the pair hook up with teenage junkie Gabe (Jacqueline McKenzie), who suggests robbing the mansion of her rich and sexually abusive father (Alex Scott). But the girl's presence begins to drive a wedge between the longtime friends.",
                            Director = "Geoffrey Wright",
                            Year = 1992,
                            Genre = "Drama, Crime",
                            Poster = "Romper_Stomper_US_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Glengarry Glen Ross",
                            Description = "When an office full of New York City real estate salesmen is given the news that all but the top two will be fired at the end of the week, the atmosphere begins to heat up. Shelley Levene (Jack Lemmon), who has a sick daughter, does everything in his power to get better leads from his boss, John Williamson (Kevin Spacey), but to no avail. When his coworker Dave Moss (Ed Harris) comes up with a plan to steal the leads, things get complicated for the tough-talking salesmen.",
                            Director = "James Foley",
                            Year = 1992,
                            Genre = "Drama",
                            Poster = "Glengarrymovie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Unforgiven",
                            Description = "When prostitute Delilah Fitzgerald (Anna Thomson) is disfigured by a pair of cowboys in Big Whiskey, Wyoming, her fellow brothel workers post a reward for their murder, much to the displeasure of sheriff Little Bill Daggett (Gene Hackman), who doesn't allow vigilantism in his town. Two groups of gunfighters, one led by aging former bandit William Munny (Clint Eastwood), the other by the florid English Bob (Richard Harris), come to collect the reward, clashing with each other and the sheriff.",
                            Director = "Clint Eastwood",
                            Year = 1992,
                            Genre = "Western",
                            Poster = "Unforgiven_2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Aileen Wuornos: The Selling of a Serial Killer",
                            Description = "In this documentary, filmmaker Nick Broomfield follows the saga of Aileen Wuornos, a prostitute who has been accused of committing a brutal series of murders. Broomfield conducts interviews with Wuornos herself, and his crew films her trial as well as her interactions with religious fanatic Arlene Pralle, who gives Wuornos dubious advice and legally adopts her. The cameras also roll as the accused's attorney ignores the case at hand to negotiate a deal to sell his client's story.",
                            Director = "Nick Broomfield",
                            Year = 1992,
                            Genre = "Documentary",
                            Poster = "AileenWuornosTheSellingOfASerialKiller1993Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Conte D’hiver",
                            Description = "A woman (Charlotte Véry) has an affair with her boss (Michel Voletti) and a librarian but longs for the man (Frédéric van den Driessche) she met five years ago.",
                            Director = "Éric Rohmer",
                            Year = 1992,
                            Genre = "Romance",
                            Poster = "ConteDHiver1992Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Yuen Ling-Yuk",
                            Description = "International superstar Maggie Cheung (In the Mood for Love) embodies tragic screen siren Ruan Lingyu, known as the 'Greta Garbo of China,' in this unconventional biopic by Hong Kong New Wave master Stanley Kwan (Rouge). Praised for her moving and emotive onscreen presence, Ruan's private life, which was frequent fodder for the vicious Shanghai tabloids, began to mirror the melodramas which brought her fame, culminating in her suicide at age 24. Kwan and Cheung paint a kaleidoscopic yet intimate portrait of the ill-fated actress, deftly blending lush period drama, archival footage, and metatextual documentary sequences of Cheung reflecting on Ruan's legacy. The result is, much like the films of Ruan Lingyu themselves, 'tender, vivid and almost overwhelmingly moving' (Time Out).",
                            Director = "Stanley Kwan Kam-Pang",
                            Year = 1992,
                            Genre = "Drama",
                            Poster = "Centre-Stage-poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "C’est Arrivé Près De Chez Vous",
                            Description = "The activities of rampaging, indiscriminate serial killer Ben (Benoît Poelvoorde) are recorded by a willingly complicit documentary team, who eventually become his accomplices and active participants. Ben provides casual commentary on the nature of his work and arbitrary musings on topics of interest to him, such as music or the conditions of low-income housing, and even goes so far as to introduce the documentary crew to his family. But their reckless indulgences soon get the better of them.",
                            Director = "Rémy Belvaux, André Bonzel",
                            Year = 1992,
                            Genre = "Drama, Crime, Comedy",
                            Poster = "Man_Bites_Dog_-_censored_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Crying Game",
                            Description = "Irish Republican Army member Fergus (Stephen Rea) forms an unexpected bond with Jody (Forest Whitaker), a kidnapped British soldier in his custody, despite the warnings of fellow IRA members Jude (Miranda Richardson) and Maguire (Adrian Dunbar). Jody makes Fergus promise he'll visit his girlfriend, Dil (Jaye Davidson), in London, and when Fergus flees to the city, he seeks her out. Hounded by his former IRA colleagues, he finds himself increasingly drawn to the enigmatic, and surprising, Dil.",
                            Director = "Neil Jordan",
                            Year = 1992,
                            Genre = "Mystery & Thriller",
                            Poster = "Crying_game_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Ba Wang Bie Ji",
                            Description = "Douzi (Leslie Cheung) is sent to an operatic school by his mother. Shitou (Fengyi Zhang), also a student at the school, befriends him, and the two endure abuse by the wretched school staff. As they hone their talents, Douzi develops romantic feelings toward Shitou, however his love is not reciprocated. When Shitou is tricked into marrying a young prostitute named Juxian (Gong Li) to save her from her tawdry life, the friendship falters and changes.",
                            Director = "Chen Kaige",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "Farewell_My_Concubine_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Groundhog Day",
                            Description = "Phil (Bill Murray), a weatherman, is out to cover the annual emergence of the groundhog from its hole. He gets caught in a blizzard that he didn't predict and finds himself trapped in a time warp. He is doomed to relive the same day over and over again until he gets it right.",
                            Director = "Harold Ramis",
                            Year = 1993,
                            Genre = "Romance",
                            Poster = "Groundhog_Day_(movie_poster).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Thirty-Two Short Films About Glenn Gould",
                            Description = "In this episodic fusion of documentary and biographical drama, director François Girard explores the life of 20th-century pianist Glenn Gould through a series of brief fictional reenactments and real interviews with those who knew the musician. After distinguishing himself as a prodigy at a very young age, the enigmatic Gould (Colm Feore) grows up to dissect the accepted rules of musical theory, and eventually refuses to perform before an audience. He also becomes addicted to prescription pills.",
                            Director = "François Girard",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "Thirty_two_short_films_about_glenn_gould_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Short Cuts",
                            Description = "Many loosely connected characters cross paths in this film, based on the stories of Raymond Carver. Waitress Doreen Piggot (Lily Tomlin) accidentally runs into a boy with her car. Soon after walking away, the child lapses into a coma. While at the hospital, the boy's grandfather (Jack Lemmon) tells his son, Howard (Bruce Davison), about his past affairs. Meanwhile, a baker (Lyle Lovett) starts harassing the family when they fail to pick up the boy's birthday cake.",
                            Director = "Robert Altman",
                            Year = 1993,
                            Genre = "Drama, Comedy",
                            Poster = "Shortcutsfilm.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Philadelphia",
                            Description = "Fearing it would compromise his career, lawyer Andrew Beckett (Tom Hanks) hides his homosexuality and HIV status at a powerful Philadelphia law firm. But his secret is exposed when a colleague spots the illness's telltale lesions. Fired shortly afterwards, Beckett resolves to sue for discrimination, teaming up with Joe Miller (Denzel Washington), the only lawyer willing to help. In court, they face one of his ex-employer's top litigators, Belinda Conine (Mary Steenburgen).",
                            Director = "Jonathan Demme",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "Philadelphia_imp.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hsimeng Jensheng",
                            Description = "An 84-year-old man reminisces about his days as a puppeteer and when he was forced to work Japanese propaganda into his acts.",
                            Director = "Hsiao-hsien Hou",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "The_Puppet_Master.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Jurassic Park",
                            Description = "In Steven Spielberg's massive blockbuster, paleontologists Alan Grant (Sam Neill) and Ellie Sattler (Laura Dern) and mathematician Ian Malcolm (Jeff Goldblum) are among a select group chosen to tour an island theme park populated by dinosaurs created from prehistoric DNA. While the park's mastermind, billionaire John Hammond (Richard Attenborough), assures everyone that the facility is safe, they find out otherwise when various ferocious predators break free and go on the hunt.",
                            Director = "Steven Spielberg",
                            Year = 1993,
                            Genre = "Sci-Fi, Action, Adventure, Mystery & Thriller",
                            Poster = "Jurassic_Park_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Trois Couleurs: Bleu",
                            Description = "Julie (Juliette Binoche) is haunted by her grief after living through a tragic auto wreck that claimed the life of her composer husband and young daughter. Her initial reaction is to withdraw from her relationships, lock herself in her apartment and suppress her pain. But avoiding human interactions on the bustling streets of Paris proves impossible, and she eventually meets up with Olivier (Benoît Régent), an old friend who harbors a secret love for her, and who could draw her back to reality.",
                            Director = "Krzysztof Kieslowski",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "Bluevidcov.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Piano",
                            Description = "After a long voyage from Scotland, pianist Ada McGrath (Holly Hunter) and her young daughter, Flora (Anna Paquin), are left with all their belongings, including a piano, on a New Zealand beach. Ada, who has been mute since childhood, has been sold into marriage to a local man named Alisdair Stewart (Sam Neill). Making little attempt to warm up to Alisdair, Ada soon becomes intrigued by his Maori-friendly acquaintance, George Baines (Harvey Keitel), leading to tense, life-altering conflicts.",
                            Director = "Jane Campion",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "The-piano-poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Lan Feng Zheng",
                            Description = "In 1950s China, just after Chen Shujuan (Lu Liping) and and her librarian husband, Lin Shaolong (Quanxin Pu), have their first child, Shaolong is unjustly forced into a labor camp as a result of Mao's purges. Shaolong dies during his imprisonment, and Shujuan marries Li Guodong (Li Xuejian). But the family faces dire poverty under the Communist regime, and the malnourished Guodong dies. Shujuan and her now adolescent son, Tietou (Chen Xiaoman), must then stick together to survive.",
                            Director = "Zhuangzhuang Tian",
                            Year = 1993,
                            Genre = "Drama",
                            Poster = "Blue_kite_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hsi Yen",
                            Description = "Wai-Tung (Winston Chao) and his boyfriend (Mitchell Lichtenstein) live happily as a gay couple in New York City. Wai-Tung has not been open about his sexuality with his Taiwanese parents (Sihung Lung, Ah-Leh Gua), and decides to acquiesce to their wish for a traditional Chinese union by marrying Wei-Wei (May Chin), a struggling artist desperate for a green card. But the simple arrangement turns into a lavish debacle when Wai-Tung's parents plan an extravagant wedding banquet.",
                            Director = "Ang Lee",
                            Year = 1993,
                            Genre = "Comedy",
                            Poster = "The_Wedding_Banquet_1993_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Schindler’s List",
                            Description = "Businessman Oskar Schindler (Liam Neeson) arrives in Krakow in 1939, ready to make his fortune from World War II, which has just started. After joining the Nazi party primarily for political expediency, he staffs his factory with Jewish workers for similarly pragmatic reasons. When the SS begins exterminating Jews in the Krakow ghetto, Schindler arranges to have his workers protected to keep his factory in operation, but soon realizes that in so doing, he is also saving innocent lives.",
                            Director = "Steven Spielberg",
                            Year = 1993,
                            Genre = "History, Drama",
                            Poster = "Schindler's_List_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Adventures of Priscilla, Queen of The Desert",
                            Description = "When drag queen Anthony (Hugo Weaving) agrees to take his act on the road, he invites fellow cross-dresser Adam (Guy Pearce) and transsexual Bernadette (Terence Stamp) to come along. In their colorful bus, named Priscilla, the three performers travel across the Australian desert performing for enthusiastic crowds and homophobic locals. But when the other two performers learn the truth about why Anthony took the job, it threatens their act and their friendship.",
                            Director = "Stephan Elliott",
                            Year = 1994,
                            Genre = "Comedy",
                            Poster = "Priscilla_the_Queen.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Trois Couleurs: Rouge",
                            Description = "Part-time model Valentine (Irène Jacob) meets a retired judge (Jean-Louis Trintignant) who lives in her neighborhood after she runs over his dog. At first the judge gifts Valentine with the dog, but her possessive boyfriend won't allow her to keep it. When she returns with the dog to the judge's house, she discovers him listening in on his neighbors' phone conversations. At first Valentine is outraged, but her debates with the judge over his behavior soon leads them to form a strange bond.",
                            Director = "Krzysztof Kieslowski",
                            Year = 1994,
                            Genre = "Drama",
                            Poster = "Three_Colors-Red.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hoop Dreams",
                            Description = "Every school day, African-American teenagers William Gates and Arthur Agee travel 90 minutes each way from inner-city Chicago to St. Joseph High School in Westchester, Illinois, a predominately white suburban school well-known for the excellence of its basketball program. Gates and Agee dream of NBA stardom, and with the support of their close-knit families, they battle the social and physical obstacles that stand in their way. This acclaimed documentary was shot over the course of five years.",
                            Director = "Steve James",
                            Year = 1994,
                            Genre = "Drama, Documentary",
                            Poster = "Hoop_dreamsposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Forrest Gump",
                            Description = "Slow-witted Forrest Gump (Tom Hanks) has never thought of himself as disadvantaged, and thanks to his supportive mother (Sally Field), he leads anything but a restricted life. Whether dominating on the gridiron as a college football star, fighting in Vietnam or captaining a shrimp boat, Forrest inspires people with his childlike optimism. But one person Forrest cares about most may be the most difficult to save -- his childhood love, the sweet but troubled Jenny (Robin Wright).",
                            Director = "Robert Zemeckis",
                            Year = 1994,
                            Genre = "Drama, Comedy",
                            Poster = "Forrest_Gump_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Lion King",
                            Description = "This Disney animated feature follows the adventures of the young lion Simba (Jonathan Taylor Thomas), the heir of his father, Mufasa (James Earl Jones). Simba's wicked uncle, Scar (Jeremy Irons), plots to usurp Mufasa's throne by luring father and son into a stampede of wildebeests. But Simba escapes, and only Mufasa is killed. Simba returns as an adult (Matthew Broderick) to take back his homeland from Scar with the help of his friends Timon (Nathan Lane) and Pumbaa (Ernie Sabella).",
                            Director = "Roger Allers, Rob Minkoff",
                            Year = 1994,
                            Genre = "Musical, Animation, Adventure, Kids & Family",
                            Poster = "The_Lion_King_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Clerks ",
                            Description = "Dante (Brian O'Halloran) is called in to cover a shift at his New Jersey convenience store on his day off. His friend Randal (Jeff Anderson) helps him pass the time, neglecting his video-store customers next door to hang out in the Quick Stop. The uneventful day is disrupted by news that one of Dante's ex-girlfriends has died. After attending her memorial service, Dante muses over staying with current girlfriend Veronica (Marilyn Ghigliotti) or reuniting with ex Caitlin (Lisa Spoonhauer).",
                            Director = "Kevin Smith",
                            Year = 1994,
                            Genre = "Comedy",
                            Poster = "Clerks_movie_poster;_Just_because_they_serve_you_---_.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Four Weddings and a Funeral",
                            Description = "Lovable Englishman Charles (Hugh Grant) and his group of friends seem to be unlucky in love. When Charles meets a beautiful American named Carrie (Andie MacDowell) at a wedding, he thinks his luck may have changed. But, after one magical night, Carrie returns to the States, ending what might have been. As Charles and Carrie's paths continue to cross -- over a handful of nuptials and one funeral -- he comes to believe they are meant to be together, even if their timing always seems to be off.",
                            Director = "Mike Newell",
                            Year = 1994,
                            Genre = "Romance",
                            Poster = "Four_weddings_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Natural Born Killers",
                            Description = "Woody Harrelson and Juliette Lewis are two young, attractive serial killers who become tabloid-TV darlings, thanks to a sensationalistic press led by Robert Downey Jr. The press reports the pair as they go on a 52 people killing spree. A controversial look at the way the media portrays criminals.",
                            Director = "Oliver Stone",
                            Year = 1994,
                            Genre = "Drama, Crime",
                            Poster = "Natural_Born_Killers.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Last Seduction",
                            Description = "Looking to escape her unhappy marriage, villainous femme fatale Bridget Gregory (Linda Fiorentino) convinces her husband, Clay (Bill Pullman), to sell cocaine, then steals the profits and runs out on him. She stops in a small town en route to Chicago, where she ensnares her next conquest, insurance man Mike Swale (Peter Berg). After getting a job at his insurance company, Bridget convinces Mike to run a scam -- but things take a deadly turn when she recruits him to help get rid of her husband.",
                            Director = "John Dahl",
                            Year = 1994,
                            Genre = "Mystery & Thriller",
                            Poster = "The-Last-Seduction-Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Pulp Fiction",
                            Description = "Vincent Vega (John Travolta) and Jules Winnfield (Samuel L. Jackson) are hitmen with a penchant for philosophical discussions. In this ultra-hip, multi-strand crime movie, their storyline is interwoven with those of their boss, gangster Marsellus Wallace (Ving Rhames) ; his actress wife, Mia (Uma Thurman) ; struggling boxer Butch Coolidge (Bruce Willis) ; master fixer Winston Wolfe (Harvey Keitel) and a nervous pair of armed robbers, 'Pumpkin' (Tim Roth) and 'Honey Bunny' (Amanda Plummer).",
                            Director = "Quentin Tarantino",
                            Year = 1994,
                            Genre = "Drama, Crime",
                            Poster = "Pulp_Fiction_(1994)_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Shawshank Redemption",
                            Description = "Andy Dufresne (Tim Robbins) is sentenced to two consecutive life terms in prison for the murders of his wife and her lover and is sentenced to a tough prison. However, only Andy knows he didn't commit the crimes. While there, he forms a friendship with Red (Morgan Freeman), experiences brutality of prison life, adapts, helps the warden, etc., all in 19 years.",
                            Director = "Frank Darabont",
                            Year = 1994,
                            Genre = "Drama",
                            Poster = "ShawshankRedemptionMoviePoster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Les Roseaux Sauvages",
                            Description = "At a boarding school in the south of France, timid student Francois (Gaël Morel) discovers his latent homosexuality when he enters into an erotic relationship with farm boy Serge (Jacques Nolot). A bizarre love triangle soon forms between Francois, his best friend Maetie (Élodie Bouchez) and Serge while the French Algerian war rages in the background, paralleling the tumult of self-discovery that comes at the crossroads between youth and adulthood.",
                            Director = "André Téchiné",
                            Year = 1994,
                            Genre = "Drama",
                            Poster = "Les_roseaux_sauvages.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Chong Qing Sen Lin",
                            Description = "Every day, Cop 223 (Takeshi Kaneshiro) buys a can of pineapple with an expiration date of May 1, symbolizing the day he'll get over his lost love. He's also got his eye on a mysterious woman in a blond wig (Brigitte Lin), oblivious of the fact she's a drug dealer. Cop 663 (Tony Leung Chiu Wai) is distraught with heartbreak over a breakup. But when his ex drops a spare set of his keys at a local cafe, a waitress (Faye Wong) lets herself into his apartment and spruces up his life.",
                            Director = "Kar-Wai Wong",
                            Year = 1994,
                            Genre = "Comedy",
                            Poster = "Chungking_Express.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Crumb",
                            Description = "Filmmaker Terry Zwigoff creates a complex but affectionate portrait of his longtime friend, underground cartoonist Robert Crumb. A notorious curmudgeon who would prefer to be alone with his fellow cartoonist wife Aline Kominsky-Crumb and his beloved vintage jazz records, Crumb reveals himself to be a complicated personality who suffered a troubled upbringing and harbors a philosophical opposition to the 1960s hippie underground that first celebrated his work.",
                            Director = "Terry Zwigoff",
                            Year = 1994,
                            Genre = "Documentary",
                            Poster = "Crumb_Movie_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Sátántangó",
                            Description = "In Bela Tarr's seven-hour episodic film, inhabitants of a small village in Hungary deal with the effects of the fall of Communism. The town's source of revenue, a factory, has closed, and the locals, who include a doctor (Putyi Horvath) and three couples, await a cash payment offered in the wake of the shuttering. Irimias (Mihaly Vig), a villager thought to be dead, returns and, unbeknownst to the locals, is a police informant. In a scheme, he persuades the villagers to form a commune with him.",
                            Director = "Béla Tarr",
                            Year = 1994,
                            Genre = "Drama, Comedy",
                            Poster = "Sátántangó_dvd_cover.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Zire Darakhatan Zeyton",
                            Description = "An Iranian director (Mohammad Ali Keshavarz) acts as go-between when his lead actors (Hossein Rezai, Tahereh Ladanian) won't work together because of cultural differences.",
                            Director = "Abbas Kiarostami",
                            Year = 1994,
                            Genre = "Comedy, Drama",
                            Poster = "Through_the_Olive_Trees_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Heavenly Creatures",
                            Description = "Wealthy and precocious teenager Juliet (Kate Winslet) transfers from England to Christchurch, New Zealand, with her family, and forms a bond with the quiet, brooding Pauline (Melanie Lynskey) through their shared love of handsome big screen tenor Mario Lanza and games of make believe. But when their parents begin to suspect that their increasingly intense friendship is becoming unhealthy, the girls decide to run away to America, hatching a dark plan for those who threaten to keep them apart.",
                            Director = "Peter Jackson",
                            Year = 1994,
                            Genre = "Mystery & Thriller",
                            Poster = "Heavenly_Creatures_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Caro Diario",
                            Description = "Filmmaker Nanni Moretti (Nanni Moretti) goes on three disparate journeys. First, he rides through Rome on a scooter, musing on cinema, and has a chance encounter with actress Jennifer Beals (Jennifer Beals). Next, he and his friend, Gerardo (Renato Carpentieri), tour a number of islands searching for a peaceful place to write a screenplay. And finally, Moretti, hampered by a nagging skin rash, goes from doctor to doctor looking for the right diagnosis, which may or may not turn out to be severe.",
                            Director = "Nanni Moretti",
                            Year = 1994,
                            Genre = "Biography, Comedy, Drama",
                            Poster = "Caro_diario_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Muriel’s Wedding",
                            Description = "Socially awkward Muriel Heslop (Toni Collette) wants nothing more than to get married. Unfortunately, due to her oppressive politician father (Bill Hunter), Muriel has never even been on a date. Ostracized by her more socially adept friends, Muriel runs into fellow outcast Rhonda Epinstalk (Rachel Griffiths), and the two move from their small Australian town to the big city of Sydney, where Muriel changes her name and begins the arduous task of redesigning her life to match her fantasies.",
                            Director = "P.J. Hogan",
                            Year = 1994,
                            Genre = "Romance",
                            Poster = "Muriels_wedding_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Riget",
                            Description = "The Kingdom is the most technologically advanced hospital in Denmark, a gleaming bastion of medical science. A rash of uncanny occurrences, however, begins to weaken the staff's faith in science--a phantom ambulance pulls in every night, but disappears; voices echo in the elevator shaft; and a pregnant doctor's fetus seems to be developing much faster than is natural. At the goading of a spiritualist patient, some employees work to let supernatural forces rest.",
                            Director = "Lars von Trier",
                            Year = 1994,
                            Genre = "Horror, Mystery & Thriller",
                            Poster = "Rigetposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Babe",
                            Description = "Gentle farmer Arthur Hoggett (James Cromwell) wins a piglet named Babe (Christine Cavanaugh) at a county fair. Narrowly escaping his fate as Christmas dinner when Farmer Hoggett decides to show him at the next fair, Babe bonds with motherly border collie Fly (Miriam Margolyes) and discovers that he too can herd sheep. But will the other farm animals, including Fly's jealous husband Rex, accept a pig who doesn't conform to the farm's social hierarchy?",
                            Director = "Chris Noonan",
                            Year = 1995,
                            Genre = "Comedy, Kids & Family, Drama, Fantasy",
                            Poster = "Babe_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Deseret",
                            Description = "Filmmaker James Benning looks at 150 years of Utah's history.",
                            Director = "James Benning",
                            Year = 1995,
                            Genre = "Documentary",
                            Poster = "Deseretposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Braveheart",
                            Description = "Tells the story of the legendary thirteenth century Scottish hero named William Wallace (Mel Gibson). Wallace rallies the Scottish against the English monarch and Edward I (Peter Hanly) after he suffers a personal tragedy by English soldiers. Wallace gathers a group of amateur warriors that is stronger than any English army.",
                            Director = "Mel Gibson",
                            Year = 1995,
                            Genre = "Drama, War, Biography, History",
                            Poster = "Braveheart_imp.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Safe",
                            Description = "Environmental illness sends a California wife (Julianne Moore) to a New Age guru's (Peter Friedman) clinic in New Mexico.",
                            Director = "Todd Haynes",
                            Year = 1995,
                            Genre = "Drama",
                            Poster = "Safe_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Toy Story",
                            Description = "Woody (Tom Hanks), a good-hearted cowboy doll who belongs to a young boy named Andy (John Morris), sees his position as Andy's favorite toy jeopardized when his parents buy him a Buzz Lightyear (Tim Allen) action figure. Even worse, the arrogant Buzz thinks he's a real spaceman on a mission to return to his home planet. When Andy's family moves to a new house, Woody and Buzz must escape the clutches of maladjusted neighbor Sid Phillips (Erik von Detten) and reunite with their boy.",
                            Director = "John Lasseter",
                            Year = 1995,
                            Genre = "Fantasy, Adventure, Comedy, Kids & Family, Animation",
                            Poster = "Toy_Story.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Casino",
                            Description = "In early-1970s Las Vegas, low-level mobster Sam 'Ace' Rothstein (Robert De Niro) gets tapped by his bosses to head the Tangiers Casino. At first, he's a great success in the job, but over the years, problems with his loose-cannon enforcer Nicky Santoro (Joe Pesci), his ex-hustler wife Ginger (Sharon Stone), her con-artist ex Lester Diamond (James Woods) and a handful of corrupt politicians put Sam in ever-increasing danger. Martin Scorsese directs this adaptation of Nicholas Pileggi's book.",
                            Director = "Martin Scorsese",
                            Year = 1995,
                            Genre = "Drama, Crime",
                            Poster = "Casino_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Heat",
                            Description = "Master criminal Neil McCauley (Robert De Niro) is trying to control the rogue actions of one of his men, while also planning one last big heist before retiring. Meanwhile, Lieutenant Hanna (Al Pacino) attempts to track down McCauley as he deals with the chaos in his own life, including the infidelity of his wife (Diane Venora) and the mental health of his stepdaughter (Natalie Portman). McCauley and Hanna discover a mutual respect, even as they try to thwart each other's plans.",
                            Director = "Michael Mann",
                            Year = 1995,
                            Genre = "Drama, Crime",
                            Poster = "Heatposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Kjærlighetens Kjøtere",
                            Description = "After his girlfriend turns down his marriage proposal, sensitive poet Henrik Larsen (Garb B. Eidsvold) becomes a fur trapper and is sent to Greenland. There he inhabits a small, filthy cabin with the brutish veteran Randbaek (Stellan Skarsgard) and the wily scientist Holm (Bjørn Sundquist). Randbaek takes sadistic delight in taunting Henrik with graphic tales of his girlfriend's infidelities, and tensions among the three men mount as they struggle to survive in the punishing conditions.",
                            Director = "Hans Petter Moland",
                            Year = 1995,
                            Genre = "Drama",
                            Poster = "Kjærlighetenskjøtere.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Clueless",
                            Description = "Shallow, rich and socially successful Cher (Alicia Silverstone) is at the top of her Beverly Hills high school's pecking scale. Seeing herself as a matchmaker, Cher first coaxes two teachers into dating each other. Emboldened by her success, she decides to give hopelessly klutzy new student Tai (Brittany Murphy) a makeover. When Tai becomes more popular than she is, Cher realizes that her disapproving ex-stepbrother (Paul Rudd) was right about how misguided she was -- and falls for him.",
                            Director = "Amy Heckerling",
                            Year = 1995,
                            Genre = "Comedy",
                            Poster = "Clueless_film_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Smoke",
                            Description = "Writer Paul Benjamin (William Hurt) is nearly hit by a bus when he leaves Auggie Wren's (Harvey Keitel) smoke shop. Stranger Rashid Cole (Harold Perrineau Jr.) saves his life, and soon middle-aged Paul tells homeless Rashid that he wouldn't mind a short-term housemate. Still grieving over his wife's murder, Paul is moved by both Rashid's quest to reconnect with his father (Forest Whitaker) and Auggie's discovery that a woman who might be his daughter is about to give birth.",
                            Director = "Wayne Wang, John Landis",
                            Year = 1995,
                            Genre = "Drama",
                            Poster = "Smoke_(movie_poster).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Badkonake Sefid",
                            Description = "It's New Year's Eve in Tehran, Iran, where it's a tradition to buy or catch a fish. Seven-year-old Razieh (Aida Mohammadkhani) yearns for a new goldfish for her family's pond, but in 90 minutes all the shops will close for a week-long holiday. After she and her brother, Ali (Mohsen Kalifi), convince their mother (Fereshteh Sadr Orfani) to give them the family's last 500 tomans, they must make it to the market in time, ward off shady characters looking to prey on them and hang on to the money.",
                            Director = "Jafar Panahi",
                            Year = 1995,
                            Genre = "Drama",
                            Poster = "Whiteballoon82.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Se7en",
                            Description = "When retiring police Detective William Somerset (Morgan Freeman) tackles a final case with the aid of newly transferred David Mills (Brad Pitt), they discover a number of elaborate and grizzly murders. They soon realize they are dealing with a serial killer (Kevin Spacey) who is targeting people he thinks represent one of the seven deadly sins. Somerset also befriends Mills' wife, Tracy (Gwyneth Paltrow), who is pregnant and afraid to raise her child in the crime-riddled city.",
                            Director = "David Fincher",
                            Year = 1995,
                            Genre = "Crime, Drama, Mystery & Thriller",
                            Poster = "Seven_(movie)_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Underground",
                            Description = "Black marketeers Marko (Miki Manojlovic) and Blacky (Lazar Ristovski) manufacture and sell weapons to the Communist resistance in WWII Belgrade, living the good life along the way. Marko's surreal duplicity propels him up the ranks of the Communist Party, and he eventually abandons Blacky and steals his girlfriend. After a lengthy stay in a below-ground shelter, the couple reemerges during the Yugoslavian Civil War of the 1990s as Marko realizes that the situation is ripe for exploitation.",
                            Director = "Emir Kusturica",
                            Year = 1995,
                            Genre = "Drama",
                            Poster = "Underground_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Xich Lo",
                            Description = "An 18-year-old orphan who is only known by his profession of a cyclo (pedal-taxi) driver, struggles on the streets of Ho Chi Minh City. When his vehicle is stolen and he is forced to pay it back he takes jobs from a local crime boss. He doesn't know that the crime boss is also his sister's pimp.",
                            Director = "Tran Anh Hung",
                            Year = 1995,
                            Genre = "Drama, Crime",
                            Poster = "Cyclo_film_cover.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Usual Suspects",
                            Description = "'The greatest trick the devil ever pulled was convincing the world he didn't exist,' says con man Kint (Kevin Spacey), drawing a comparison to the most enigmatic criminal of all time, Keyser Soze. Kint attempts to convince the feds that the mythic crime lord not only exists, but is also responsible for drawing Kint and his four partners into a multi-million dollar heist that ended with an explosion in San Pedro Harbor - leaving few survivors.",
                            Director = "Bryan Singer",
                            Year = 1995,
                            Genre = "Drama, Crime, Mystery & Thriller",
                            Poster = "Usual_suspects_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Dead Man",
                            Description = "Circumstances transform a mild-mannered accountant (Johnny Depp) into a notorious Old West gunslinger.",
                            Director = "Jim Jarmusch",
                            Year = 1995,
                            Genre = "Western",
                            Poster = "DeadManPoster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Fargo",
                            Description = "'Fargo' is a reality-based crime drama set in Minnesota in 1987. Jerry Lundegaard (William H. Macy) is a car salesman in Minneapolis who has gotten himself into debt and is so desperate for money that he hires two thugs (Steve Buscemi), (Peter Stormare) to kidnap his own wife. Jerry will collect the ransom from her wealthy father (Harve Presnell), paying the thugs a small portion and keeping the rest to satisfy his debts. The scheme collapses when the thugs shoot a state trooper.",
                            Director = "Joel Coen",
                            Year = 1996,
                            Genre = "Comedy, Drama, Mystery & Thriller, Crime",
                            Poster = "Fargo_(1996_movie_poster).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Trois Vies & Une Seule Mort",
                            Description = "The same characters experience different realities across four separate stories in this experimental exploration of truth and identity. In one, Parisian salesman Mateo Strano (Marcello Mastroianni) abandons his wife, Maria (Marisa Paredes), then reemerges two decades later. In another, a professor becomes a panhandler. Next, a butler serves a young couple that has inherited a large house. And finally, a business mogul is confronted by a family he thought he had made up.",
                            Director = "Raoul Ruiz",
                            Year = 1996,
                            Genre = "Drama, Comedy",
                            Poster = "Trois_vies_et_une_seule_mort_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Shine",
                            Description = "As a child piano prodigy, David Helfgott's (Geoffrey Rush) musical ambitions generate friction with his overbearing father, Peter (Armin Mueller-Stahl). When Helfgott travels to London on a musical scholarship, his career as a pianist blossoms. However, the pressures of his newfound fame, coupled with the echoes of his tumultuous childhood, conspire to bring Helfgott's latent schizophrenia boiling to the surface, and he spends years in and out of various mental institutions.",
                            Director = "Scott Hicks",
                            Year = 1996,
                            Genre = "Biography, Drama",
                            Poster = "Shine_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Breaking The Waves",
                            Description = "In a small and religious coastal town, a simple, devoutly religious Scottish woman, Bess McNeill (Emily Watson), finds a partner in an oil rig worker from Norway, Jan Nyman (Stellan Skarsgard). However, the relationship grows strained when Nyman breaks his neck in a horrific work accident on the rig and becomes paralyzed. Unable to perform sexually and suffering mentally from the accident as well, Jan convinces Bess to have sex with other men, which she comes to believe is God's work.",
                            Director = "Lars von Trier",
                            Year = 1996,
                            Genre = "Drama",
                            Poster = "Breaking_the_Waves_(Danish_poster).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Independence Day",
                            Description = "In the epic adventure film 'Independence Day,' strange phenomena surface around the globe. The skies ignite. Terror races through the world's major cities. As these extraordinary events unfold, it becomes increasingly clear that a force of incredible magnitude has arrived; its mission: total annihilation over the Fourth of July weekend. The last hope to stop the destruction is an unlikely group of people united by fate and unimaginable circumstances.",
                            Director = "Roland Emmerich",
                            Year = 1996,
                            Genre = "Sci-Fi, Action, Adventure",
                            Poster = "Independence_day_movieposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Secrets & Lies",
                            Description = "After her adoptive mother dies, Hortense (Marianne Jean-Baptiste), a successful black eye doctor, seeks out her birth mother. She's shocked when her research leads her to a lower-class white woman, Cynthia (Brenda Blethyn). At first Cynthia denies the claim, but she eventually admits to birthing Hortense as a teenager, and the two begin to bond. However, when Cynthia invites Hortense to a family barbecue, Cynthia's already tense relationship with her family becomes even more complicated.",
                            Director = "Mike Leigh",
                            Year = 1996,
                            Genre = "Drama",
                            Poster = "Secrets-and-lies-movie-poster-1996-UK.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Gabbeh",
                            Description = "In this Iranian romantic fantasy, an elderly married couple (Hossein Moharami, Roghieh Moharami) head to the river to wash their gabbeh, a traditional Persian carpet with a colorful illustration of a young woman woven into it. Suddenly, the figure in the design (Shaghayegh Djodat) springs miraculously to life, declares herself to be called Gabbeh, and then spins a story about her history, her family and the man she loved but was forbidden to marry.",
                            Director = "Mohsen Makhmalbaf",
                            Year = 1996,
                            Genre = "Drama",
                            Poster = "Gabbeh_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Lone Star",
                            Description = "In the Texas border town of Frontera, Sheriff Sam Deeds (Chris Cooper) digs up the past when he finds an old skull in the desert. As he traces the murder of Sheriff Charlie Wade (Kris Kristofferson) 40 years earlier, Deeds' investigation points toward his late father, the much-loved Deputy Buddy Deeds. Ignoring warnings not to delve any deeper, Sam rekindles a romance with his high school sweetheart while bringing up old tensions in the town and exposing secrets long put to rest.",
                            Director = "John Sayles",
                            Year = 1996,
                            Genre = "Drama",
                            Poster = "Lone_Star_film.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Trainspotting",
                            Description = "Heroin addict Mark Renton (Ewan McGregor) stumbles through bad ideas and sobriety attempts with his unreliable friends -- Sick Boy (Jonny Lee Miller), Begbie (Robert Carlyle), Spud (Ewen Bremner) and Tommy (Kevin McKidd). He also has an underage girlfriend, Diane (Kelly Macdonald), along for the ride. After cleaning up and moving from Edinburgh to London, Mark finds he can't escape the life he left behind when Begbie shows up at his front door on the lam, and a scheming Sick Boy follows.",
                            Director = "Danny Boyle",
                            Year = 1996,
                            Genre = "Drama, Comedy",
                            Poster = "Trainspotting_ver2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Scream",
                            Description = "The sleepy little town of Woodsboro just woke up screaming. There's a killer in their midst who's seen a few too many scary movies. Suddenly nobody is safe, as the psychopath stalks victims, taunts them with trivia questions, then rips them to bloody shreds. It could be anybody...",
                            Director = "Wes Craven",
                            Year = 1996,
                            Genre = "Horror, Comedy",
                            Poster = "Scream_movie_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The English Patient",
                            Description = "The sweeping expanses of the Sahara are the setting for a passionate love affair in this adaptation of Michael Ondaatje's novel. A badly burned man, Laszlo de Almasy (Ralph Fiennes), is tended to by a nurse, Hana (Juliette Binoche), in an Italian monastery near the end of World War II. His past is revealed through flashbacks involving a married Englishwoman (Kristin Scott Thomas) and his work mapping the African landscape. Hana learns to heal her own scars as she helps the dying man.",
                            Director = "Anthony Minghella",
                            Year = 1996,
                            Genre = "Drama",
                            Poster = "The_English_Patient_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Ice Storm",
                            Description = "In the 1970s, an outwardly wholesome family begins cracking at the seams over the course of a tumultuous Thanksgiving break. Frustrated with his job, the father, Ben (Kevin Kline), seeks fulfillment by cheating on his wife, Elena (Joan Allen), with neighborhood seductress Janey (Sigourney Weaver). Their teenage daughter, Wendy (Christina Ricci), dabbles in sexual affairs too -- with Janey's son Mikey (Elijah Wood). The family's strained relations continue to tauten until an ice storm strikes.",
                            Director = "Ang Lee",
                            Year = 1997,
                            Genre = "Drama",
                            Poster = "The_Ice_Storm_(film).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Sweet Hereafter",
                            Description = "A small mountain community in Canada is devastated when a school bus accident leaves more than a dozen of its children dead. A big-city lawyer (Ian Holm) arrives to help the survivors' and victims' families prepare a class-action suit, but his efforts only seem to push the townspeople further apart. At the same time, one teenage survivor of the accident (Sarah Polley) has to reckon with the loss of innocence brought about by a different kind of damage.",
                            Director = "Atom Egoyan",
                            Year = 1997,
                            Genre = "Drama",
                            Poster = "The_Sweet_Hereafter_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Ta’m E Guilass",
                            Description = "A middle-aged Tehranian man, Mr. Badii (Homayoun Ershadi) is intent on killing himself and seeks someone to bury him after his demise. Driving around the city, the seemingly well-to-do Badii meets with numerous people, including a Muslim student (Mir Hossein Noori), asking them to take on the job, but initially he has little luck. Eventually, Badii finds a man who is up for the task because he needs the money, but his new associate soon tries to talk him out of committing suicide.",
                            Director = "Abbas Kiarostami",
                            Year = 1997,
                            Genre = "Drama",
                            Poster = "Tasteofcherryposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Funny Games",
                            Description = "An idyllic lakeside vacation home is terrorized by Paul (Arno Frisch) and Peter (Frank Giering), a pair of deeply disturbed young men. When the fearful Anna (Susanne Lothar) is home alone, the two men drop by for a visit that quickly turns violent and terrifying. Husband Georg (Ulrich Mühe) comes to her rescue, but Paul and Peter take the family hostage and subject them to nightmarish abuse and humiliation. From time to time, Paul talks to the film's audience, making it complicit in the horror.",
                            Director = "Michael Haneke",
                            Year = 1997,
                            Genre = "Mystery & Thriller",
                            Poster = "Funny_Games1997.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Titanic",
                            Description = "James Cameron's 'Titanic' is an epic, action-packed romance set against the ill-fated maiden voyage of the R.M.S. Titanic; the pride and joy of the White Star Line and, at the time, the largest moving object ever built. She was the most luxurious liner of her era -- the 'ship of dreams' -- which ultimately carried over 1,500 people to their death in the ice cold waters of the North Atlantic in the early hours of April 15, 1912.",
                            Director = "James Cameron",
                            Year = 1997,
                            Genre = "Romance, Drama, History",
                            Poster = "Titanic_(Official_Film_Poster).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Abre Los Ojos",
                            Description = "Handsome 25-year-old Cesar (Eduardo Noriega) had it all -- a successful career, expensive cars, a swank bachelor's pad, and an endless string of beautiful and willing women. He is then thrown into a strange psychological mystery after a car accident scars his face and lands him in prison.",
                            Director = "Alejandro Amenábar",
                            Year = 1997,
                            Genre = "Drama, Mystery & Thriller",
                            Poster = "Abre_los_ojos_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "L.A. Confidential",
                            Description = "Three policemen, each with his own motives and obsessions, tackle the corruption surrounding an unsolved murder at a downtown Los Angeles coffee shop in the early 1950s. Detective Lieutenant Exley (Guy Pearce), the son of a murdered detective, is out to avenge his father's killing. The ex-partner of Officer White (Russell Crowe), implicated in a scandal rooted out by Exley, was one of the victims. Sergeant Vincennes (Kevin Spacey) feeds classified information to a tabloid magnate (Danny DeVito).",
                            Director = "Curtis Hanson",
                            Year = 1997,
                            Genre = "Drama, Crime",
                            Poster = "La_confidential.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Saving Private Ryan",
                            Description = "Captain John Miller (Tom Hanks) takes his men behind enemy lines to find Private James Ryan, whose three brothers have been killed in combat. Surrounded by the brutal realties of war, while searching for Ryan, each man embarks upon a personal journey and discovers their own strength to triumph over an uncertain future with honor, decency and courage.",
                            Director = "Steven Spielberg",
                            Year = 1998,
                            Genre = "Drama, War, History",
                            Poster = "Saving_Private_Ryan_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Buffalo 66",
                            Description = "Convict Billy Brown (Vincent Gallo) dreads going home so much upon his release from prison that he tries to get back inside. In desperation, Billy kidnaps Layla (Christina Ricci) from a tap dancing class and pleads with her to impersonate his wife and to accompany him home to visit his parents, Janet (Anjelica Huston) and Jimmy (Ben Gazzara). To Billy's dismay, Layla takes to her role enthusiastically. She breaks through to obsessive Buffalo Bills football fan Janet and the hard-edged Jimmy.",
                            Director = "Vincent Gallo",
                            Year = 1998,
                            Genre = "Drama",
                            Poster = "Buffalo_sixty_six_ver1.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Rushmore",
                            Description = "When a beautiful first-grade teacher (Olivia Williams) arrives at a prep school, she soon attracts the attention of an ambitious teenager named Max (Jason Schwartzman), who quickly falls in love with her. Max turns to the father (Bill Murray) of two of his schoolmates for advice on how to woo the teacher. However, the situation soon gets complicated when Max's new friend becomes involved with her, setting the two pals against one another in a war for her attention.",
                            Director = "Wes Anderson",
                            Year = 1998,
                            Genre = "Comedy",
                            Poster = "Rushmoreposter.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Lola Rennt",
                            Description = "In this visually and conceptually impressive film, two-bit Berlin criminal Manni (Moritz Bleibtreu) delivers some smuggled loot for his boss, Ronnie (Heino Ferch), but accidentally leaves the 100,000 mark payment in a subway car. Given 20 minutes to come up with the money, he calls his girlfriend, Lola (Franka Potente), who sprints through the streets of the city to try to beg the money out of her bank manager father (Herbert Knaup) and get to Manni before he does something desperate.",
                            Director = "Tom Tykwer",
                            Year = 1998,
                            Genre = "Action, Mystery & Thriller",
                            Poster = "Lola_Rennt_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Happiness",
                            Description = "This dark ensemble-comedy is centered on the three Jordan sisters. Joy (Jane Adams) moves through lackluster jobs with no sense of purpose. Now employed teaching adults, she is dating a student, Russian taxi-driver Vlad (Jared Harris). Helen (Lara Flynn Boyle) is an esteemed poet who becomes amused by her perverted neighbor, Allen (Philip Seymour Hoffman). And eldest sister Trish (Cynthia Stevenson) is married to Bill (Dylan Baker), a psychiatrist with a very disturbing secret life.",
                            Director = "Todd Solondz",
                            Year = 1998,
                            Genre = "Drama, Comedy",
                            Poster = "Happiness1998Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Pi",
                            Description = "Numbers whiz Max Cohen (Sean Gullette) is stunted by psychological delusions of paranoia and debilitating headaches. He lives in a messy Chinatown apartment, where he tinkers with equations and his homemade, super-advanced computer. One day, however, Cohen encounters a mysterious number. Soon after reporting his discovery to his mentor (Mark Margolis) and to a religious friend (Ben Shenkman), he finds himself the target of ill-intentioned Wall Street agents bent on using the number for profit.",
                            Director = "Darren Aronofsky",
                            Year = 1998,
                            Genre = "Drama",
                            Poster = "Piposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Festen",
                            Description = "A dark secret mars a Dane's (Henning Moritzen) 60th birthday celebration, attended by his family (Ulrich Thomsen, Thomas Bo Larsen) and friends.",
                            Director = "Thomas Vinterberg",
                            Year = 1998,
                            Genre = "Comedy, Drama",
                            Poster = "The_Celebration_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Ring",
                            Description = "When her niece is found dead along with three friends after viewing a supposedly cursed videotape, reporter Reiko Asakawa (Nanako Matsushima) sets out to investigate. Along with her ex-husband, Ryuji (Hiroyuki Sanada), Reiko finds the tape, watches it -- and promptly receives a phone call informing her that she'll die in a week. Determined to get to the bottom of the curse, Reiko and Ryuji discover the video's origin and attempt to solve an old murder that could break the spell.",
                            Director = "Hideo Nakata",
                            Year = 1998,
                            Genre = "Mystery & Thriller, Horror",
                            Poster = "Ringu_(1998)_Japanese_theatrical_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Thin Red Line",
                            Description = "In 1942, Private Witt (Jim Caviezel) is a U.S. Army absconder living peacefully with the locals of a small South Pacific island. Discovered by his commanding officer, Sgt. Welsh (Sean Penn), Witt is forced to resume his active duty training for the Battle of Guadalcanal. As Witt and his unit land on the island, and the American troops mount an assault on entrenched Japanese positions, the story explores their various fates and attitudes towards life-or-death situations.",
                            Director = "Terrence Malick",
                            Year = 1998,
                            Genre = "War, History, Drama",
                            Poster = "The_Thin_Red_Line_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Three Kings",
                            Description = "Just after the end of the Gulf War, four American soldiers decide to steal a cache of Saddam Hussein's hidden gold. Led by cynical Sergeant Major Archie Gates (George Clooney), three of the men are rescued by rebels, but Sergeant Troy Barlow (Mark Wahlberg) is captured and tortured by Iraqi intelligence. The Iraqi rebels beg for the American trio to help fight against the impending arrival of Hussein's Elite Guard. The men agree to fight in return for help rescuing Troy.",
                            Director = "David O. Russell",
                            Year = 1999,
                            Genre = "War, Comedy, Drama",
                            Poster = "Three_Kings_(film)_poster_art.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Magnolia",
                            Description = "On one random day in the San Fernando Valley, a dying father, a young wife, a male caretaker, a famous lost son, a police officer in love, a boy genius, an ex-boy genius, a game show host and an estranged daughter will each become part of a dazzling multiplicity of plots, but one story.",
                            Director = "Paul Thomas Anderson",
                            Year = 1999,
                            Genre = "Drama",
                            Poster = "Magnolia_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Fight Club",
                            Description = "A depressed man (Edward Norton) suffering from insomnia meets a strange soap salesman named Tyler Durden (Brad Pitt) and soon finds himself living in his squalid house after his perfect apartment is destroyed. The two bored men form an underground club with strict rules and fight other men who are fed up with their mundane lives. Their perfect partnership frays when Marla (Helena Bonham Carter), a fellow support group crasher, attracts Tyler's attention.",
                            Director = "David Fincher",
                            Year = 1999,
                            Genre = "Drama, Mystery & Thriller",
                            Poster = "Fight_Club_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Audition",
                            Description = "This disturbing Japanese thriller follows Aoyama (Ryo Ishibashi), a widower who decides to start dating again. Aided by a film-producer friend (Jun Kunimura), Aoyama uses auditions for a fake production to function as a dating service. When Aoyama becomes intrigued by the withdrawn, gorgeous Asami (Eihi Shiina), they begin a relationship. However, he begins to realize that Asami isn't as reserved as she appears to be, leading to gradually increased tension and a harrowing climax.",
                            Director = "Takashi Miike",
                            Year = 1999,
                            Genre = "Horror",
                            Poster = "Audition-1999-poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Blair Witch Project",
                            Description = "Found video footage tells the tale of three film students (Heather Donahue, Joshua Leonard, Michael C. Williams) who've traveled to a small town to collect documentary footage about the Blair Witch, a legendary local murderer. Over the course of several days, the students interview townspeople and gather clues to support the tale's veracity. But the project takes a frightening turn when the students lose their way in the woods and begin hearing horrific noises.",
                            Director = "Daniel Myrick, Eduardo Sánchez",
                            Year = 1999,
                            Genre = "Mystery & Thriller, Horror",
                            Poster = "Blair_Witch_Project.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Todo Sobre Mi Madre",
                            Description = "A Greek saying states that only women who have washed their eyes with tears can see clearly. This saying does not hold true for Manuela. The night a car ran over her son Esteban, Manuela cried until her eyes ran completely dry. Far from seeing clearly, the present and the future become mixed up in darkness. She begins looking for his father who has become a transvestite.",
                            Director = "Pedro Almodóvar",
                            Year = 1999,
                            Genre = "Drama, Comedy",
                            Poster = "All_about_my_mother.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Beau Travail",
                            Description = "Foreign Legion officer, Galoup, recalls his once glorious life, leading troops in the Gulf of Djibouti. His existence there was happy, strict and regimented, but the arrival of a promising young recruit, Sentain, plants the seeds of jealousy in Galoup's mind. He feels compelled to stop him from coming to the attention of the commandant who he admires, but who ignores him. Ultimately, his jealousy leads to the destruction of both Sentain and himself.",
                            Director = "Claire Denis",
                            Year = 1999,
                            Genre = "Drama",
                            Poster = "Beau_Travail_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Being John Malkovich",
                            Description = "In this quirky cult-favorite comedy, unemployed New York City puppeteer Craig Schwartz (John Cusack) reluctantly takes a temp job as a filing clerk for the eccentric Dr. Lester (Orson Bean). While at work, Craig discovers a portal that leads into the mind of renowned actor John Malkovich. When he lets his attractive co-worker Maxine (Catherine Keener) in on the secret, they begin both an unusual business scheme and an odd relationship that involves Craig's restless wife, Lotte (Cameron Diaz).",
                            Director = "Spike Jonze",
                            Year = 1999,
                            Genre = "Comedy",
                            Poster = "Being_John_Malkovich_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "American Beauty",
                            Description = "Lester Burnham (Kevin Spacey) is a gainfully employed suburban husband and father. Fed up with his boring, stagnant existence, he quits his job and decides to reinvent himself as a pot-smoking, responsibility-shirking teenager. What follows is at once cynical, hysterical, and, eventually, tragically uplifting.",
                            Director = "Sam Mendes",
                            Year = 1999,
                            Genre = "Drama, Comedy",
                            Poster = "American_Beauty_1999_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Matrix",
                            Description = "Neo (Keanu Reeves) believes that Morpheus (Laurence Fishburne), an elusive figure considered to be the most dangerous man alive, can answer his question -- What is the Matrix? Neo is contacted by Trinity (Carrie-Anne Moss), a beautiful stranger who leads him into an underworld where he meets Morpheus. They fight a brutal battle for their lives against a cadre of viciously intelligent secret agents. It is a truth that could cost Neo something more precious than his life.",
                            Director = "Lilly Wachowski, Lana Wachowski",
                            Year = 1999,
                            Genre = "Action, Sci-Fi",
                            Poster = "The_Matrix_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Sixth Sense",
                            Description = "Young Cole Sear (Haley Joel Osment) is haunted by a dark secret: he is visited by ghosts. Cole is frightened by visitations from those with unresolved problems who appear from the shadows. He is too afraid to tell anyone about his anguish, except child psychologist Dr. Malcolm Crowe (Bruce Willis). As Dr. Crowe tries to uncover the truth about Cole's supernatural abilities, the consequences for client and therapist are a jolt that awakens them both to something unexplainable.",
                            Director = "M. Night Shyamalan",
                            Year = 1999,
                            Genre = "Mystery & Thriller",
                            Poster = "The_Sixth_Sense_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Contents Les Glaneurs Et La Glaneuse",
                            Description = "An 1867 painting by Jean-Francois Millet inspired septuagenarian documentarian Agnes Varda to cross the French countryside to videotape people who scavenge. Taking everything from surplus in the fields, to rubbish in trashcans, to oysters washed up after a storm, the 'gleaners' range from those sadly in need to those hoping to recreate the community activity of centuries past, and still others who use whatever they find to cobble together a rough art. Highlighted by Varda's amusing narration.",
                            Director = "Agnès Varda",
                            Year = 2000,
                            Genre = "Documentary",
                            Poster = "Les_glaneurs_et_la_glaneuse_(film).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Gladiator",
                            Description = "Commodus (Joaquin Phoenix) takes power and strips rank from Maximus (Russell Crowe), one of the favored generals of his predecessor and father, Emperor Marcus Aurelius, the great stoical philosopher. Maximus is then relegated to fighting to the death in the gladiator arenas.",
                            Director = "Ridley Scott",
                            Year = 2000,
                            Genre = "Adventure, Action, History, Drama",
                            Poster = "Gladiator_(2000_film_poster).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Dut Yeung Nin Wa",
                            Description = "In 1962, journalist Chow Mo-wan (Tony Leung Chiu Wai) and his wife move into a Hong Kong apartment, but Chow's spouse is often away on business. Before long, the lonely Chow makes the acquaintance of the alluring Su Li-zhen (Maggie Cheung Man-yuk), whose own significant other also seems preoccupied with work. As the two friends realize their respective partners are cheating on them, they begin to fall for one another; however, neither wants to stoop to the level of the unfaithful spouses.",
                            Director = "Kar-Wai Wong",
                            Year = 2000,
                            Genre = "Romance, Drama",
                            Poster = "In_the_Mood_for_Love_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Kippur",
                            Description = "A first-hand witness to the 1973 Yom Kippur War, in which troops from Egypt and Syria chose one of the holiest days of the Jewish calendar to launch a surprise attack on Israel. This short but bloody conflict is seen through the eyes of a student who has been instructed to join a special military unit on the Golan Heights shortly after the fighting begins, but they are thrown in an emergency medical team.",
                            Director = "Amos Gitai",
                            Year = 2000,
                            Genre = "War",
                            Poster = "Kippur_film.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Yi Yi",
                            Description = "Set in Taiwan, the film follows the lives of the Jian family from the alternating perspectives of the three main family members: father N.J. (Nien-Jen Wu), teenage daughter Ting-Ting (Kelly Lee) and young son Yang-Yang (Jonathan Chang). N.J., disgruntled with his current job, attempts to court the favor of a prominent video game company while Ting-Ting and Yang-Yang contend with the various trials of youth, all while caring for N.J.'s mother-in-law, who lies in a coma.",
                            Director = "Edward Yang",
                            Year = 2000,
                            Genre = "Drama",
                            Poster = "Yiyiposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Requiem for a Dream",
                            Description = "Imaginatively evoking the inner landscape of human beings longing to connect, to love and feel loved, the film is a parable of happiness gloriously found and tragically lost. 'Requiem for a Dream' tells parallel stories that are linked by the relationship between the lonely, widowed Sara Goldfarb and her sweet but aimless son, Harry. The plump Sara, galvanized by the prospect of appearing on a TV game show, has started on a dangerous diet regimen to beautify herself for a national audience.",
                            Director = "Darren Aronofsky",
                            Year = 2000,
                            Genre = "Drama",
                            Poster = "Requiem_for_a_dream.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Amores Perros",
                            Description = "'Amores Perros' is a bold, intensely emotional, and ambitious story of lives that collide in a Mexico City car crash. Inventively structured as a triptych of overlapping and intersecting narratives, 'Amores Perros' explores the lives of disparate characters who are catapulted into unforeseen dramatic situations instigated by the seemingly inconsequential destiny of a dog named Cofi.",
                            Director = "Alejandro González Iñárritu",
                            Year = 2000,
                            Genre = "Mystery & Thriller, Drama",
                            Poster = "Amores_Perros_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Dancer In The Dark",
                            Description = "Selma is a Czech immigrant, a single mother working in a factory in rural America. Her salvation is passion for music, specifically, the all-singing, all-dancing numbers found in classic Hollywood musicals. Selma harbors a sad secret: she is losing her eyesight and her son Gene stands to suffer the same fate if she can't put away enough money to secure him an operation. When a desperate neighbor falsely accuses Selma of stealing his savings, the drama of her life escalates to a tragic finale.",
                            Director = "Lars von Trier",
                            Year = 2000,
                            Genre = "Drama",
                            Poster = "Dancer_in_the_Dark_(Danish_poster).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Wo Hu Cang Long",
                            Description = "In 19th century Qing Dynasty China, a warrior (Chow Yun-Fat) gives his sword, Green Destiny, to his lover (Michelle Yeoh) to deliver to safe keeping, but it is stolen, and the chase is on to find it. The search leads to the House of Yu where the story takes on a whole different level.",
                            Director = "Ang Lee",
                            Year = 2000,
                            Genre = "Action, Sports & Fitness, Adventure",
                            Poster = "Crouching_Tiger,_Hidden_Dragon_(Chinese_poster).png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Memento",
                            Description = "Leonard (Guy Pearce) is tracking down the man who raped and murdered his wife. The difficulty, however, of locating his wife's killer is compounded by the fact that he suffers from a rare, untreatable form of memory loss. Although he can recall details of life before his accident, Leonard cannot remember what happened fifteen minutes ago, where he's going, or why.",
                            Director = "Christopher Nolan",
                            Year = 2000,
                            Genre = "Mystery & Thriller",
                            Poster = "Memento_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Sen to Chihiro No Kamikakushi",
                            Description = "10-year-old Chihiro (Daveigh Chase) moves with her parents to a new home in the Japanese countryside. After taking a wrong turn down a wooded path, Chihiro and her parents discover an amusement park with a stall containing an assortment of food. To her surprise, Chihiro's parents begin eating and then transform into pigs. In this supernatural realm, Chihiro encounters a host of characters and endures labor in a bathhouse for spirits, awaiting a reunion with her parents.",
                            Director = "Hayao Miyazaki, Kirk Wise",
                            Year = 2001,
                            Genre = "Adventure, Animation, Fantasy",
                            Poster = "Spirited_Away_Japanese_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Lord of The Rings",
                            Description = "The future of civilization rests in the fate of the One Ring, which has been lost for centuries. Powerful forces are unrelenting in their search for it. But fate has placed it in the hands of a young Hobbit named Frodo Baggins (Elijah Wood), who inherits the Ring and steps into legend. A daunting task lies ahead for Frodo when he becomes the Ringbearer - to destroy the One Ring in the fires of Mount Doom where it was forged.",
                            Director = "Peter Jackson",
                            Year = 2001,
                            Genre = "Adventure, Fantasy",
                            Poster = "The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_(2001).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Safar E Ghandehar",
                            Description = "Nafas is a young Afghan journalist who has taken refuge in Canada. She receives a desperate letter from her little sister, who has stayed behind in Afghanistan and has decided to end her life before the imminently approaching eclipse of the sun. Nafas fled her country during the Taliban civil war. She decides to go and help her sister in Kandahar and attempts to cross the Iran-Afghanistan border...",
                            Director = "Mohsen Makhmalbaf",
                            Year = 2001,
                            Genre = "Drama",
                            Poster = "Kandahar_(2001_film).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "No Man’s Land",
                            Description = "Ciki (Branko Djuric) and Nino (Rene Bitorajac), a Bosnian and a Serb, are soldiers stranded in No Man's Land -- a trench between enemy lines during the Bosnian war. They have no one to trust, no way to escape without getting shot, and a fellow soldier is lying on the trench floor with a spring-loaded bomb set to explode beneath him if he moves. The absurdity of their situation would be comical if it didn't have such dire consequences.",
                            Director = "Danis Tanović",
                            Year = 2001,
                            Genre = "Comedy, War",
                            Poster = "No_Man's_Land_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Cidade De Deus",
                            Description = "In the poverty-stricken favelas of Rio de Janeiro in the 1970s, two young men choose different paths. Rocket (Phellipe Haagensen) is a budding photographer who documents the increasing drug-related violence of his neighborhood. José 'Zé' Pequeno (Douglas Silva) is an ambitious drug dealer who uses Rocket and his photos as a way to increase his fame as a turf war erupts with his rival, 'Knockout Ned' (Leandro Firmino da Hora). The film was shot on location in Rio's poorest neighborhoods.",
                            Director = "Fernando Meirelles, Kátia Lund",
                            Year = 2002,
                            Genre = "Crime, Drama",
                            Poster = "CidadedeDeus.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Uzak",
                            Description = "After losing his factory job, Yusuf (Mehmet Emin Toprak) leaves his Turkish village and travels to Istanbul in search of work. There, he lives with his cousin Mahmut (Muzaffer Özdemir), a well-to-do photographer. Yusuf, who assumed it would be easy to secure a position aboard a ship, has little luck in his job search. As the days go by, Mahmut clashes with his countrified cousin over their vast differences in personality -- and, perhaps more so, their uncomfortable similarities.",
                            Director = "Nuri Bilge Ceylan",
                            Year = 2002,
                            Genre = "Drama",
                            Poster = "Uzak.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Hable Con Ella",
                            Description = "Male nurse Benigno (Javier Cámara) becomes infatuated with a complete stranger when he watches dancer Alicia (Leonor Watling) practicing from the anonymity of his apartment. After being injured in a car accident, Alicia is brought to a nearby hospital, where Benigno serendipitously happens to be her caregiver. When wounded bullfighter Lydia (Rosario Flores) is brought into the same ward, her companion, writer Marco (Darío Grandinetti), begins to bond with Benigno.",
                            Director = "Pedro Almodóvar",
                            Year = 2002,
                            Genre = "Romance, Drama",
                            Poster = "Talk_to_Her_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Pianist",
                            Description = "In this adaptation of the autobiography 'The Pianist: The Extraordinary True Story of One Man's Survival in Warsaw, 1939-1945,' Wladyslaw Szpilman (Adrien Brody), a Polish Jewish radio station pianist, sees Warsaw change gradually as World War II begins. Szpilman is forced into the Warsaw Ghetto, but is later separated from his family during Operation Reinhard. From this time until the concentration camp prisoners are released, Szpilman hides in various locations among the ruins of Warsaw.",
                            Director = "Roman Polanski",
                            Year = 2002,
                            Genre = "War, History, Drama",
                            Poster = "The_Pianist_movie.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Oldboy",
                            Description = "Dae-Su is an obnoxious drunk bailed from the police station yet again by a friend. However, he's abducted from the street and wakes up in a cell, where he remains for the next 15 years, drugged unconscious when human contact is unavoidable, otherwise with only the television as company. And then, suddenly released, he is invited to track down his jailor with a denouement that is simply stunning.",
                            Director = "Park Chan-wook",
                            Year = 2003,
                            Genre = "Mystery & Thriller",
                            Poster = "Oldboykoreanposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Good Bye Lenin!",
                            Description = "In October 1989, right before the fall of the Berlin Wall, Alex Kerner (Daniel Brühl) is living with his mom, Christiane (Kathrin Sass), and sister, Ariane (Maria Simon). But when the mother, a loyal party member, sees Alex participating in an anti-communist rally, she falls into a coma and misses the revolution. After she wakes, doctors say any jarring event could make her have a heart attack, meaning the family must go to great lengths to pretend communism still reigns in Berlin.",
                            Director = "Wolfgang Becker",
                            Year = 2003,
                            Genre = "Comedy, Drama",
                            Poster = "Good_Bye_Lenin.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "La Meglio Gioventù",
                            Description = "Two close-knit brothers, Nicola (Luigi Lo Cascio) and Matteo (Alessio Boni), are buffeted by the social and political upheavals that rock their native Italy during the 1960s and '70s as they navigate their way into adulthood, marriage and middle age. Along the way, Nicola meets Giulia (Sonia Bergamasco), the love of his life whose political beliefs take precedence over her personal happiness, while Matteo falls for the lovely photographer-turned-librarian Mirella (Maya Sansa).",
                            Director = "Marco Tullio Giordana",
                            Year = 2003,
                            Genre = "Drama",
                            Poster = "BestofYouth.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Passion of The Christ",
                            Description = "In this version of Christ's crucifixion, based on the New Testament, Judas expedites the downfall of Jesus (Jim Caviezel) by handing him over to the Roman Empire's handpicked officials. To the horror of his mother, Mary (Maia Morgenstern), Magdalen (Monica Bellucci), whom he saved from damnation, and his disciples, Jesus is condemned to death. He is tortured as he drags a crucifix to nearby Calvary, where he is nailed to the cross. He dies, but not before a last act of grace.",
                            Director = "Mel Gibson",
                            Year = 2004,
                            Genre = "Drama",
                            Poster = "",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Gegen Die Wand",
                            Description = "Cahit (Birol Ünel) stumbles from bar to bar in Hamburg, Germany, lost in an alcoholic haze. The boozy Turkish immigrant crashes his car into a wall, which lands him in a mental institution. There he meets the suicidal Sibel (Sibel Kekilli). She convinces him to marry her, because otherwise her family will arrange her marriage to a Turkish man of their choosing. She proposes a deal to Cahit: She will cook and clean for him and they can see other people. Thus begins their strange romance.",
                            Director = "Fatih Akin",
                            Year = 2004,
                            Genre = "Drama",
                            Poster = "Gegen_die_Wand_(2004).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Der Untergang",
                            Description = "In 1942, young Traudl Junge (Alexandra Maria Lara) lands her dream job -- secretary to Adolf Hitler (Bruno Ganz) at the peak of his power. Three years later, Hitler's empire is now his underground bunker. The real-life Traudl narrates Hitler's final days as he rages against imagined betrayers and barks orders to phantom armies, while his mistress, Eva Braun (Juliane Köhler), clucks over his emotional distance, and other infamous Nazis prepare for the end.",
                            Director = "Oliver Hirschbiegel",
                            Year = 2004,
                            Genre = "Drama, War, History",
                            Poster = "Der_Untergang_Downfall_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Paradise Now",
                            Description = "Khaled (Ali Suliman) and Said (Kais Nashif) are Palestinian friends recruited by a terrorist group to become suicide bombers in Tel Aviv. Armed with explosives, they attempt to cross into Israel, but are pursued by suspicious border guards. Khaled returns to the terrorists, while Said sneaks into Israel and ponders detonating at another target. After Khaled and Said reunite to begin their mission again, Khaled has reconsidered, and tries to convince Said to give up the bombing as well.",
                            Director = "Hany Abu-Assad",
                            Year = 2005,
                            Genre = "Drama",
                            Poster = "Paradisenowfilm.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Brokeback Mountain",
                            Description = "In 1963, rodeo cowboy Jack Twist (Jake Gyllenhaal) and ranch hand Ennis Del Mar (Heath Ledger) are hired by rancher Joe Aguirre (Randy Quaid) as sheep herders in Wyoming. One night on Brokeback Mountain, Jack makes a drunken pass at Ennis that is eventually reciprocated. Though Ennis marries his longtime sweetheart, Alma (Michelle Williams), and Jack marries a fellow rodeo rider (Anne Hathaway), the two men keep up their tortured and sporadic affair over the course of 20 years.",
                            Director = "Ang Lee",
                            Year = 2005,
                            Genre = "Romance, Drama",
                            Poster = "Brokeback_mountain.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Tsotsi",
                            Description = "A South African hoodlum named Tsotsi (Presley Chweneyagae) lives by a code of violence, and he and his gang of thugs prowl the streets of Johannesburg day and night, attacking those who fail to give them what they want. After casually shooting a woman and stealing her car, he discovers her baby in the back seat. Instead of harming the mewling infant, he takes it home and cares for it. The child acts as a catalyst for the hardened thug to regain his humanity.",
                            Director = "Gavin Hood",
                            Year = 2005,
                            Genre = "Drama",
                            Poster = "Tsotsiposter.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Little Miss Sunshine",
                            Description = "The Hoover family -- a man (Greg Kinnear), his wife (Toni Collette), an uncle (Steve Carell), a brother (Paul Dano) and a grandfather (Alan Arkin) -- puts the fun back in dysfunctional by piling into a VW bus and heading to California to support a daughter (Abigail Breslin) in her bid to win the Little Miss Sunshine Contest. The sanity of everyone involved is stretched to the limit as the group's quirks cause epic problems as they travel along their interstate route.",
                            Director = "Jonathan Dayton, Valerie Faris",
                            Year = 2006,
                            Genre = "Comedy, Drama",
                            Poster = "Little_miss_sunshine_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Departed",
                            Description = "South Boston cop Billy Costigan (Leonardo DiCaprio) goes under cover to infiltrate the organization of gangland chief Frank Costello (Jack Nicholson). As Billy gains the mobster's trust, a career criminal named Colin Sullivan (Matt Damon) infiltrates the police department and reports on its activities to his syndicate bosses. When both organizations learn they have a mole in their midst, Billy and Colin must figure out each other's identities to save their own lives.",
                            Director = "Martin Scorsese",
                            Year = 2006,
                            Genre = "Crime, Mystery & Thriller, Drama",
                            Poster = "Departed234.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "El Laberinto Del Fauno",
                            Description = "In 1944 Spain young Ofelia (Ivana Baquero) and her ailing mother (Ariadna Gil) arrive at the post of her mother's new husband (Sergi López), a sadistic army officer who is trying to quell a guerrilla uprising. While exploring an ancient maze, Ofelia encounters the faun Pan, who tells her that she is a legendary lost princess and must complete three dangerous tasks in order to claim immortality.",
                            Director = "Guillermo del Toro",
                            Year = 2006,
                            Genre = "War, Fantasy, Drama",
                            Poster = "Pan's_Labyrinth.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Borat: Cultural Learnings of America for Make Benefit Glorious Nation of Kazakhstan",
                            Description = "Outrageous situations occur when Borat, a popular reporter (Sacha Baron Cohen) from Kazakhstan, comes to the United States to film a documentary on what makes America a great nation. Along the way, he manages to offend just about everyone he meets, fall in love with actress Pamela Anderson, and set forth on a cross-country journey to make her his wife.",
                            Director = "Larry Charles",
                            Year = 2006,
                            Genre = "Comedy",
                            Poster = "Borat_ver2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Das Leben Der Anderen",
                            Description = "In 1983 East Berlin, dedicated Stasi officer Gerd Wiesler (Ulrich Mühe), doubting that a famous playwright (Sebastian Koch) is loyal to the Communist Party, receives approval to spy on the man and his actress-lover Christa-Maria (Martina Gedeck). Wiesler becomes unexpectedly sympathetic to the couple, then faces conflicting loyalties when his superior takes a liking to Christa-Maria and orders Wiesler to get the playwright out of the way.",
                            Director = "Florian Henckel von Donnersmarck",
                            Year = 2006,
                            Genre = "Drama, History",
                            Poster = "Leben_der_anderen.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Once",
                            Description = "A vacuum repairman (Glen Hansard) moonlights as a street musician and hopes for his big break. One day a Czech immigrant (Marketa Irglova), who earns a living selling flowers, approaches him with the news that she is also an aspiring singer-songwriter. The pair decide to collaborate, and the songs that they compose reflect the story of their blossoming love.",
                            Director = "John Carney",
                            Year = 2006,
                            Genre = "Musical, Romance, Drama",
                            Poster = "Once_(2006_film)poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Apocalypto",
                            Description = "The Mayan kingdom is at the height of its opulence and power but the foundations of the empire are beginning to crumble. The leaders believe they must build more temples and sacrifice more people or their crops and citizens will die. Jaguar Paw (Rudy Youngblood), a peaceful hunter in a remote tribe, is captured along with his entire village in a raid. He is scheduled for a ritual sacrifice until he makes a daring escape and tries to make it back to his pregnant wife and son.",
                            Director = "Mel Gibson",
                            Year = 2006,
                            Genre = "Action, History, Adventure, Drama",
                            Poster = "Apocalypto-poster01.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "La Vie En Rose",
                            Description = "Born into poverty and raised in a brothel, Édith Piaf (Marion Cotillard) manages to achieve worldwide fame. Though her extraordinary voice and charisma open many doors that lead to friendships and romances, she experiences great personal loss, drug addiction and an early death.",
                            Director = "Olivier Dahan",
                            Year = 2007,
                            Genre = "Biography, Drama",
                            Poster = "La_Vie_en_Rose_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Paranormal Activity",
                            Description = "Ryan Fleege (Chris J. Murray), his wife Emily (Brit Shaw) and their 7-year-old daughter Leila (Ivy George) are preparing for Christmas in their new home. After finding an old and mysterious camcorder, Ryan quickly learns that it can record strange apparitions that are invisible to the naked eye. When young Leila starts talking to an imaginary friend and displaying strange behavior, the couple soon find themselves in a terrifying battle with a supernatural force.",
                            Director = "Gregory Plotkin",
                            Year = 2007,
                            Genre = "Horror, Mystery & Thriller",
                            Poster = "Paranormal_Activity_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Le Scaphandre Et Le Papillon",
                            Description = "Jean-Dominique Bauby (Mathieu Amalric), editor-in-chief of French fashion bible Elle magazine, has a devastating stroke at age 43. The damage to his brain stem results in locked-in syndrome, with which he is almost completely paralyzed and only able to communicate by blinking an eye. Bauby painstakingly dictates his memoir via the only means of expression left to him.",
                            Director = "Julian Schnabel",
                            Year = 2007,
                            Genre = "Biography, Drama",
                            Poster = "DivingBellButterflyMP.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "There Will be Blood",
                            Description = "Silver miner Daniel Plainview (Daniel Day-Lewis) leads a hardscrabble life with his son, H.W. (Dillon Freasier). When he hears about oil oozing from the ground near the Western town of Little Boston, Daniel takes his son on a mission to find their fortune. Daniel makes his lucky strike and becomes a self-made tycoon but, as his fortune grows, he deviates into moral bankruptcy.",
                            Director = "Paul Thomas Anderson",
                            Year = 2007,
                            Genre = "Drama",
                            Poster = "There_Will_Be_Blood_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "No Country for Old Men",
                            Description = "While out hunting, Llewelyn Moss (Josh Brolin) finds the grisly aftermath of a drug deal. Though he knows better, he cannot resist the cash left behind and takes it with him. The hunter becomes the hunted when a merciless killer named Chigurh (Javier Bardem) picks up his trail. Also looking for Moss is Sheriff Bell (Tommy Lee Jones), an aging lawman who reflects on a changing world and a dark secret of his own, as he tries to find and protect Moss.",
                            Director = "Joel Coen, Ethan Coen",
                            Year = 2007,
                            Genre = "Crime, Mystery & Thriller, Drama",
                            Poster = "No_Country_for_Old_Men_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Anvil! The Story of Anvil",
                            Description = "Canadian heavy-metal band Anvil delivered a highly influential 1982 album that would inspire the likes of Anthrax and Metallica, and then dropped off the map to begin what would become decades of toiling in obscurity. Director and former roadie Sacha Gervasi follows guitarist Steve 'Lips' Kudlow and drummer Robb Reiner as they stumble through a harrowing European tour and reflect on failure, friendship, resilience and the will to follow even the most impossible of dreams.",
                            Director = "Sacha Gervasi",
                            Year = 2008,
                            Genre = "Documentary",
                            Poster = "Anvil_ver2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Joheunnom Nabbeunnom Isanghannom",
                            Description = "In 1930s Manchuria, an encounter on a train triggers an epic crusade for a treasure map, prompting a marathon chase in hot pursuit of the loot. Do-won, 'The Good', is a bounty hunter, out to track down Chang-yee, 'The Bad', a charismatic hit man attempting to steal the map from a military official. However, the ruthless Tae-goo puts a hitch in both their plans when he secures the map for himself.",
                            Director = "Kim Jee-woon",
                            Year = 2008,
                            Genre = "Action, Western",
                            Poster = "The_Good,_the_Bad,_the_Weird_film_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Dark Knight",
                            Description = "With the help of allies Lt. Jim Gordon (Gary Oldman) and DA Harvey Dent (Aaron Eckhart), Batman (Christian Bale) has been able to keep a tight lid on crime in Gotham City. But when a vile young criminal calling himself the Joker (Heath Ledger) suddenly throws the town into chaos, the caped Crusader begins to tread a fine line between heroism and vigilantism.",
                            Director = "Christopher Nolan",
                            Year = 2008,
                            Genre = "Action, Adventure, Fantasy",
                            Poster = "Dark_Knight.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Slumdog Millionaire",
                            Description = "As 18-year-old Jamal Malik (Dev Patel) answers questions on the Indian version of 'Who Wants to Be a Millionaire,' flashbacks show how he got there. Part of a stable of young thieves after their mother dies, Jamal and his brother, Salim, survive on the streets of Mumbai. Salim finds the life of crime agreeable, but Jamal scrapes by with small jobs until landing a spot on the game show.",
                            Director = "Danny Boyle",
                            Year = 2008,
                            Genre = "Comedy, Drama",
                            Poster = "Slumdog_Millionaire_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Gomorra",
                            Description = "In the slums of Campania, the Camorra crime syndicate has created a fortune out of cocaine, corruption and chemical waste. Some try to fight back, like teens Ciro (Ciro Petrone) and Marco (Marco Macor), who decide to steal a Camorra weapons cache in a bid to take control themselves. Others try to hide, like Pasquale (Salvatore Cantalupo), a tailor trying to get around paying protection fees. But the realization sets in: The Camorra is too large, too deeply embedded in Italy to be fought.",
                            Director = "Matteo Garrone",
                            Year = 2008,
                            Genre = "Crime, Drama",
                            Poster = "Gomorra_(2008_movie_poster).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Låt Den Rätte Komma In",
                            Description = "When Oskar (Kåre Hedebrant), a sensitive, bullied 12-year-old boy living with his mother in suburban Sweden, meets his new neighbor, the mysterious and moody Eli (Lina Leandersson), they strike up a friendship. Initially reserved with each other, Oskar and Eli slowly form a close bond, but it soon becomes apparent that she is no ordinary young girl. Eventually, Eli shares her dark, macabre secret with Oskar, revealing her connection to a string of bloody local murders.",
                            Director = "Tomas Alfredson",
                            Year = 2008,
                            Genre = "Horror",
                            Poster = "Let_the_Right_One_In_(Swedish).jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Hurt Locker",
                            Description = "Staff Sgt. William James (Jeremy Renner), Sgt. J.T. Sanborn (Anthony Mackie) and Specialist Owen Eldridge (Brian Geraghty) are members of a bomb-disposal unit in Baghdad. As their tour of duty enters its final weeks, the men face a set of increasingly hazardous situations, any of which could end their lives in an explosive instant.",
                            Director = "Kathryn Bigelow",
                            Year = 2008,
                            Genre = "Mystery & Thriller, Action, War, Drama",
                            Poster = "HLposterUSA2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Avatar",
                            Description = "On the lush alien world of Pandora live the Na'vi, beings who appear primitive but are highly evolved. Because the planet's environment is poisonous, human/Na'vi hybrids, called Avatars, must link to human minds to allow for free movement on Pandora. Jake Sully (Sam Worthington), a paralyzed former Marine, becomes mobile again through one such Avatar and falls in love with a Na'vi woman (Zoe Saldana). As a bond with her grows, he is drawn into a battle for the survival of her world.",
                            Director = "James Cameron",
                            Year = 2009,
                            Genre = "Action, Sci-Fi, Adventure, Fantasy",
                            Poster = "Avatar-Teaser-Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "District 9",
                            Description = "Thirty years ago, aliens arrive on Earth -- not to conquer or give aid, but -- to find refuge from their dying planet. Separated from humans in a South African area called District 9, the aliens are managed by Multi-National United, which is unconcerned with the aliens' welfare but will do anything to master their advanced technology. When a company field agent (Sharlto Copley) contracts a mysterious virus that begins to alter his DNA, there is only one place he can hide: District 9.",
                            Director = "Neill Blomkamp",
                            Year = 2009,
                            Genre = "Sci-Fi",
                            Poster = "District_nine_ver2.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Hangover",
                            Description = "Two days before his wedding, Doug (Justin Bartha) and three friends (Bradley Cooper, Ed Helms, Zach Galifianakis) drive to Las Vegas for a wild and memorable stag party. In fact, when the three groomsmen wake up the next morning, they can't remember a thing; nor can they find Doug. With little time to spare, the three hazy pals try to re-trace their steps and find Doug so they can get him back to Los Angeles in time to walk down the aisle.",
                            Director = "Todd Phillips",
                            Year = 2009,
                            Genre = "Comedy",
                            Poster = "Hangoverposter09.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "In The Loop",
                            Description = "During an interview, British Cabinet Minister Simon Foster (Tom Hollander) delivers an off-the-cuff remark that war in the Middle East is 'unforeseeable.' Profane political spin doctor Malcolm Tucker (Peter Capaldi) tries to cover up Foster's faux pas, but the ill-conceived comment is picked up by a warmongering American official. Foster is invited to Washington, D.C., where a war of words brews as politicians maneuver, manipulate and deceive each other before a U.N. vote on military action.",
                            Director = "Armando Iannucci",
                            Year = 2009,
                            Genre = "Comedy",
                            Poster = "In_the_Loop_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Das Weisse Band",
                            Description = "Using their social status and harsh punishments, a baron (Ulrich Tukur), a doctor (Rainer Bock) and a pastor (Burghart Klaussner) rule over a small German village. One day, the doctor falls off his horse after it trips over a wire strung between two trees. More pranks follow, seemingly without reason, all directed at the village's upper class and growing increasingly more brutal with time. There are no suspects, but a local schoolteacher (Christian Friedel) has his suspicions.",
                            Director = "Michael Haneke",
                            Year = 2009,
                            Genre = "War, Drama",
                            Poster = "White_ribbon.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Inglourious Basterds",
                            Description = "It is the first year of Germany's occupation of France. Allied officer Lt. Aldo Raine (Brad Pitt) assembles a team of Jewish soldiers to commit violent acts of retribution against the Nazis, including the taking of their scalps. He and his men join forces with Bridget von Hammersmark, a German actress and undercover agent, to bring down the leaders of the Third Reich. Their fates converge with theater owner Shosanna Dreyfus, who seeks to avenge the Nazis' execution of her family.",
                            Director = "Quentin Tarantino",
                            Year = 2009,
                            Genre = "Comedy, War, Drama",
                            Poster = "Inglourious_Basterds_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Fish Tank",
                            Description = "Mia (Katie Jarvis) is a rebellious teenager on the verge of being kicked out of school. Her hard-partying mother, Joanne (Kierston Wareing), neglects Mia's welfare in favor of her own, and her younger sister (Rebecca Griffiths) hangs out with a much older crowd. Sparks fly between Mia and Connor (Michael Fassbender), Joanne's new boyfriend, and he encourages Mia to pursue her interest in dance. As the boundaries of the relationships become blurred, Mia and Joanne compete for Connor's affection.",
                            Director = "Andrea Arnold",
                            Year = 2009,
                            Genre = "Drama",
                            Poster = "Fish_tank_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Monsters",
                            Description = "Six years after the crash of a NASA space probe, a large area between Mexico and the U.S. is under quarantine while Mexican and American military forces fight a losing battle with the giant creatures who now live there. While covering the conflict, a photojournalist (Scoot McNairy) gets uncomfortably close to the action when he escorts his boss's daughter (Whitney Able) through the zone to the U.S. border...",
                            Director = "Gareth Edwards",
                            Year = 2010,
                            Genre = "Sci-Fi, Mystery & Thriller",
                            Poster = "MonstersUK.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Des Hommes Et Des Dieux",
                            Description = "An order of Trappist monks whose members include Christian (Lambert Wilson) and Luc (Michael Lonsdale) live among the Muslim population in a quiet corner of Algeria. As the country is plunged into civil war in the mid-1990s, the men of God must decide whether to stay among the impoverished residents who have been their neighbors, or flee the encroaching fundamentalist terrorists. The situation that unfolds, based on actual events, has tragic consequences.",
                            Director = "Xavier Beauvois",
                            Year = 2010,
                            Genre = "Drama",
                            Poster = "Hommes-dieux-poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Black Swan",
                            Description = "Nina (Natalie Portman) is a ballerina whose passion for the dance rules every facet of her life. When the company's artistic director decides to replace his prima ballerina for their opening production of 'Swan Lake,' Nina is his first choice. She has competition in newcomer Lily (Mila Kunis) however. While Nina is perfect for the role of the White Swan, Lily personifies the Black Swan. As rivalry between the two dancers transforms into a twisted friendship, Nina's dark side begins to emerge.",
                            Director = "Darren Aronofsky",
                            Year = 2010,
                            Genre = "Mystery & Thriller, Drama",
                            Poster = "Black_Swan_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Four Lions",
                            Description = "A group of young Muslim men living in Sheffield decide to wage jihad, and they hatch an inept plan to become suicide bombers. Omar (Riz Ahmed) and Waj (Kayvan Novak) have a brief, disastrous run at a Pakistan training camp, while Faisal (Adeel Akhtar) works on an unlikely scheme to train birds to carry bombs. Their ill-conceived plan culminates at the London Marathon with their bumbling attempts to disrupt the event while dressed in outlandish costumes.",
                            Director = "Christopher Morris",
                            Year = 2010,
                            Genre = "Comedy, Drama",
                            Poster = "Four_Lions_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The Social Network",
                            Description = "In 2003, Harvard undergrad and computer genius Mark Zuckerberg (Jesse Eisenberg) begins work on a new concept that eventually turns into the global social network known as Facebook. Six years later, he is one of the youngest billionaires ever, but Zuckerberg finds that his unprecedented success leads to both personal and legal complications when he ends up on the receiving end of two lawsuits, one involving his former friend (Andrew Garfield). Based on the book 'The Accidental Billionaires.'",
                            Director = "David Fincher",
                            Year = 2010,
                            Genre = "Biography, Drama",
                            Poster = "The_Social_Network_film_poster.png",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "Inception",
                            Description = "Dom Cobb (Leonardo DiCaprio) is a thief with the rare ability to enter people's dreams and steal their secrets from their subconscious. His skill has made him a hot commodity in the world of corporate espionage but has also cost him everything he loves. Cobb gets a chance at redemption when he is offered a seemingly impossible task: Plant an idea in someone's mind. If he succeeds, it will be the perfect crime, but a dangerous enemy anticipates Cobb's every move.",
                            Director = "Christopher Nolan",
                            Year = 2010,
                            Genre = "Action, Sci-Fi, Mystery & Thriller",
                            Poster = "Inception_(2010)_theatrical_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "The King’s Speech",
                            Description = "England's Prince Albert (Colin Firth) must ascend the throne as King George VI, but he has a speech impediment. Knowing that the country needs her husband to be able to communicate effectively, Elizabeth (Helena Bonham Carter) hires Lionel Logue (Geoffrey Rush), an Australian actor and speech therapist, to help him overcome his stammer. An extraordinary friendship develops between the two men, as Logue uses unconventional means to teach the monarch how to speak with confidence.",
                            Director = "Tom Hooper",
                            Year = 2010,
                            Genre = "History, Drama",
                            Poster = "The_King's_Speech_poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        },
                        new Film
                        {
                            Title = "True Grit",
                            Description = "After an outlaw named Tom Chaney (Josh Brolin) murders her father, feisty 14-year-old farm girl Mattie Ross (Hailee Steinfeld) hires Rooster Cogburn (Jeff Bridges), a boozy, trigger-happy lawman, to help her find Chaney and avenge her father. The bickering duo are not alone in their quest, for a Texas Ranger named LaBoeuf (Matt Damon) is also tracking Chaney for reasons of his own. Together the unlikely trio ventures into hostile territory to dispense some Old West justice.",
                            Director = "Joel Coen, Ethan Coen",
                            Year = 2010,
                            Genre = "Western, Drama",
                            Poster = "True_Grit_Poster.jpg",
                            Registration = DateTime.Now,
                            UserId = adminId
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
