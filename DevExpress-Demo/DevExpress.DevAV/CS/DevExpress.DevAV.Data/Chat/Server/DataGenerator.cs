#region Copyright (c) 2000-2022 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2022 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2022 Developer Express Inc.

namespace DevExpress.DevAV.Chat {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DevExpress.DevAV.Chat.Events;
	partial class DevAVEmpployeesInMemoryServer {
		static class DataGenerator {
			readonly static Random generator = new Random(10001);
			public static int GetCount(int min = 0, int max = 10) {
				return generator.Next(min, max + 1 );
			}
			public static DateTime GetLastYesterdayTime() {
				var minutes = GetCount(-1440 * 2, -1440);
				return DateTime.Now.AddMinutes(minutes);
			}
			public static HashSet<long> GetRandomIds(int all, int bottom = 17, int top = 23) {
				var ids = new HashSet<long>();
				int count = generator.Next(bottom, all - top);
				while(ids.Count < count)
					ids.Add(generator.Next(all));
				return ids;
			}
			public static HashSet<long> GetRandomIds(long[] allIds, int min = 3, int max = 7) {
				var ids = new HashSet<long>();
				int count = generator.Next(min, max);
				while(ids.Count < count)
					ids.Add(allIds[generator.Next(allIds.Length)]);
				return ids;
			}
			public static long EitherOr(long current, long other) {
				return (generator.NextDouble() > 0.5) ? current : other;
			}
		}
		static class LoremIpsum {
			public const string Explanation = "\"Lorem ipsum...\" is a dummy text used to replace text in some areas just for the purpose of an example. The words \"Lorem ipsum...\" make no sense.";
			const int CommonEntriesCount = 15;
			public static readonly string[] StaticGenerated = new[] {
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
			"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
			"Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
			"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
			"Curabitur pretium tincidunt lacus. Nulla gravida orci a odio.",
			"Nullam varius, turpis et commodo pharetra, est eros bibendum elit, nec luctus magna felis sollicitudin mauris.",
			"Integer in mauris eu nibh euismod gravida.",
			"Duis ac tellus et risus vulputate vehicula. Donec lobortis risus a elit. Etiam tempor.",
			"Ut ullamcorper, ligula eu tempor congue, eros est euismod turpis, id tincidunt sapien risus a quam. Maecenas fermentum consequat mi.",
			"Donec fermentum. Pellentesque malesuada nulla a mi.",
			"Duis sapien sem, aliquet nec, commodo eget, consequat quis, neque.",
			"Aliquam faucibus, elit ut dictum aliquet, felis nisl adipiscing sapien, sed malesuada diam lacus eget erat.",
			"Cras mollis scelerisque nunc. Nullam arcu.",
			"Aliquam consequat. Curabitur augue lorem, dapibus quis, laoreet et, pretium ac, nisi.",
			"Aenean magna nisl, mollis quis, molestie eu, feugiat in, orci. In hac habitasse platea dictumst.",
			"Duis rutrum, dui ut tempor dignissim, lectus eros scelerisque sapien, ac tincidunt est risus vel sem.",
			"Praesent tempor sodales volutpat. Ut consectetur, orci nec fermentum rutrum, odio orci convallis ipsum, vel ultricies nisl nisi et orci.",
			"Aliquam turpis lectus, fermentum et eros ut, accumsan dictum mauris.",
			"Suspendisse ut lectus eu diam dignissim elementum ut sed purus. Integer lacinia fringilla metus ac maximus.",
			"Nullam nec libero maximus, rhoncus mi eget, convallis tellus. Morbi cursus tellus ligula, ac sollicitudin risus semper sit amet.",
			"Suspendisse commodo, lorem ac dictum gravida, mauris sem aliquam nunc, eu imperdiet augue lorem sit amet nibh.",
			"Donec hendrerit leo et fringilla condimentum. Nullam ultricies faucibus iaculis.",
			"Proin aliquam turpis erat, ac luctus elit aliquet at. Nullam porta, erat vel egestas feugiat, nisi metus hendrerit enim, non bibendum odio enim eu lacus.",
			"Aenean fringilla euismod est, a fringilla tellus luctus at. Maecenas suscipit libero ornare blandit lobortis.",
			"Suspendisse ultricies sagittis lorem, a rutrum massa pharetra in.",
			"Vivamus tincidunt ante mauris, non semper nisl consectetur et. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
			"Donec vitae augue id felis bibendum aliquam. Donec blandit magna vitae nunc sollicitudin hendrerit.",
			"Morbi consectetur consequat pellentesque. Proin et erat in risus rutrum euismod.",
			"Fusce et nisi euismod, aliquam lorem sit amet, ornare orci. Sed diam augue, porta efficitur sollicitudin a, placerat aliquet quam.",
			"Aliquam nec leo sed magna condimentum tempus quis eget sem. Sed cursus dolor felis, a consequat massa pretium et.",
			"Vestibulum id ante sem.",
			"In auctor, lorem quis iaculis dictum, enim mi pharetra mi, non porttitor risus metus ut risus.",
			"Praesent sed ex quis metus malesuada egestas. Donec sit amet purus pellentesque lectus maximus hendrerit eu nec ante.",
			"Morbi tempor risus vel enim porttitor, a tempor quam rhoncus. Duis pharetra iaculis sem, quis elementum ipsum bibendum a.",
			"Cras volutpat efficitur ipsum vitae pharetra. Nunc eget tortor commodo, hendrerit nisi ac, congue felis.",
			"Nunc ut venenatis dui. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc a consequat magna.",
			"Phasellus nec lacus elit. Phasellus eget aliquam justo. Proin vel laoreet augue.",
			"Aliquam erat volutpat. Donec a nibh feugiat, efficitur augue ut, consectetur nisl.",
			"Phasellus fermentum nibh metus, ut cursus turpis consequat ac. Vestibulum at iaculis diam.",
			"Integer eget turpis euismod, hendrerit libero quis, sodales erat.",
			"Morbi faucibus purus eu enim feugiat blandit.",
			"In vel dui turpis. Proin fermentum rutrum dapibus.",
			"Phasellus velit eros, molestie eu tempor quis, mattis ac nisi.",
			"Maecenas a orci vulputate, molestie lorem sed, hendrerit mi. Aliquam sed lacus tincidunt, malesuada sem a, semper tortor.",
			"Aliquam id risus sit amet dui tempus tincidunt sed eget massa. Ut non sem massa.",
			"Nunc auctor nunc efficitur mattis fringilla. Suspendisse a nisi sed nisi porttitor suscipit et ac massa.",
			"Donec porttitor augue interdum augue vehicula, id aliquam nibh tempor.",
			"Praesent non sollicitudin sem, sed tincidunt eros. Quisque sed maximus metus.",
			"Phasellus quis ex ultricies, vestibulum quam in, finibus lorem. Duis hendrerit porta lobortis.",
			"Aenean laoreet porttitor neque ut luctus. In viverra interdum risus, quis volutpat nibh auctor sed.",
			"Praesent et interdum felis. Suspendisse sit amet dui quis libero pretium dignissim quis et mi.",
			"Etiam sed tincidunt sapien, ac mollis purus. Sed ac sodales nisi, vitae molestie urna.",
			"Maecenas eget nisl a lectus tristique porttitor at quis diam. Vestibulum aliquam convallis arcu eget convallis.",
			"Donec eros mauris, ornare a tortor a, tempus maximus risus. Integer fermentum consequat sapien, ut facilisis lacus posuere vitae.",
			"Donec ante eros, semper nec tempus sit amet, iaculis blandit sem.",
			"Nullam vel massa vel ligula gravida molestie. Phasellus ac erat malesuada, varius ipsum vel, pulvinar dui.",
			"Nunc sagittis lacus sed leo gravida gravida. Mauris pellentesque tellus est, in ultrices mauris pulvinar vel.",
			"Etiam luctus odio eu aliquam posuere. Aliquam ut velit vitae velit interdum interdum.",
			"Vivamus euismod laoreet urna gravida dictum. Praesent accumsan pellentesque est, a pellentesque ante placerat vel.",
			"Nunc sagittis tincidunt mauris, eget molestie orci pellentesque eget.",
			"Maecenas volutpat consequat sem, et commodo erat vulputate eget.",
			"In a lectus in sapien vehicula ullamcorper in vitae tortor. Aliquam venenatis placerat finibus.",
			"Proin pulvinar elit a lorem congue rhoncus. Integer hendrerit, mi et lacinia sagittis, erat nisi dignissim nisi, a elementum quam augue sit amet leo.",
			"Sed eu ullamcorper leo. Sed a felis mollis, lacinia sapien at, suscipit ipsum.",
			"Suspendisse rhoncus orci ut urna laoreet, quis ultricies justo fermentum.",
			"Donec ultrices velit quis nulla aliquet condimentum. Etiam a ultrices felis.",
			"Nam lorem nisl, varius eu fermentum a, molestie vel turpis. Pellentesque vel ex non arcu volutpat laoreet quis non est.",
			"Proin non massa nec felis fringilla tristique at et mauris. Integer ut porta metus, at finibus sem.",
			"Cras pretium viverra ex eu molestie. Vestibulum malesuada felis vitae rhoncus tempus.",
			"Suspendisse at odio non purus scelerisque lobortis. Suspendisse eget tortor neque.",
			"Vestibulum volutpat felis ac lorem tristique eleifend. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
			"Phasellus imperdiet nibh eu odio suscipit, eget vestibulum ligula condimentum.",
			"Sed volutpat ac metus vel sagittis. Etiam condimentum facilisis mi a imperdiet.",
			"Aliquam tempus justo in dictum aliquet. Nulla bibendum tempus est ac vehicula.",
			"Donec eget tincidunt massa. Donec nulla erat, venenatis nec magna non, tempus efficitur augue.",
			"Vestibulum posuere arcu et metus molestie tincidunt. Nulla metus nisi, vehicula nec posuere ac, posuere ac purus.",
			"Duis quam tellus, viverra semper nisl ut, varius bibendum sem. Sed eu pulvinar sapien.",
			"Sed euismod, nisi eget venenatis malesuada, odio tortor laoreet elit, nec rhoncus tortor purus vel arcu.",
			"Duis rutrum tellus ligula, imperdiet maximus metus gravida a. Nunc commodo velit at diam imperdiet sodales.",
			"In ex felis, aliquam ac rutrum eget, tempus vel eros. Pellentesque gravida vehicula posuere.",
			"In rutrum, lorem a gravida lobortis, felis risus suscipit nibh, faucibus rhoncus dolor ligula vitae arcu.",
			"Donec mollis eros in dui semper, eget ultrices ipsum vulputate. Ut id cursus ex.",
			"Pellentesque in ante id odio maximus volutpat. Sed diam dolor, maximus ac aliquam vel, lobortis cursus massa.",
			"Etiam quis nisl at nulla sagittis sagittis sed et quam. Aenean pharetra nisl sed elit tempus, id dapibus felis viverra.",
			"Aliquam ante lorem, consectetur at magna sed, facilisis pretium tellus.",
			"Cras a egestas tortor. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
			"Nulla vulputate enim eu libero eleifend posuere. Cras laoreet ut ex at condimentum.",
			"Vivamus eget mauris tincidunt diam bibendum suscipit. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.",
			"Mauris interdum sapien id purus gravida, non malesuada dolor blandit.",
			"Sed fringilla maximus varius. Morbi finibus accumsan elit, ac molestie elit bibendum ac.",
			"Donec euismod bibendum tellus id volutpat. Duis nec tellus augue.",
			"Integer consectetur ut quam eget interdum. Quisque et urna vel nisl volutpat gravida sit amet at nulla.",
			"Aliquam iaculis viverra turpis, nec finibus nulla convallis quis.",
			"Vivamus pretium, sapien ac molestie condimentum, mi arcu vehicula nunc, id varius eros sem quis ante.",
			"Curabitur vehicula nisl odio, eu congue erat venenatis et. Etiam at rhoncus sapien.",
			"Donec luctus ante quam. Aliquam consectetur urna vitae hendrerit efficitur.",
			"Maecenas non neque interdum, semper nulla ac, ultricies nunc. Vivamus mattis dolor egestas augue gravida, et mattis lectus consequat.",
			"Vivamus sed auctor nulla. Nulla aliquet fermentum enim vitae hendrerit.",
			"Suspendisse ipsum odio, accumsan at fringilla non, semper quis dolor.",
			"In non mollis libero, pretium suscipit ex. Duis sed purus leo. In non mauris magna.",
			"Fusce leo sapien, maximus ut finibus eget, facilisis nec erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.",
			"Suspendisse cursus venenatis nisi. Fusce interdum lacinia porttitor.",
			"Aliquam elementum lobortis ex ut luctus. Curabitur gravida nisi justo, non ullamcorper nibh tempor sit amet.",
			"Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
			"Cras a rhoncus ipsum.",
			"Vivamus eget mi est. Fusce fermentum vehicula magna at feugiat.",
			"Proin porta ullamcorper ipsum laoreet blandit. Morbi posuere, est a posuere viverra, tellus tellus consequat nulla, quis eleifend mauris est nec arcu.",
			"Morbi id massa nec nisl eleifend rhoncus aliquet et tellus. Donec sit amet luctus diam.",
			"Donec quis nibh et massa mattis convallis sed at nibh. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
			"Nunc ut massa et tortor lobortis sodales. Fusce elementum sapien at purus aliquam, vitae lobortis lacus condimentum.",
			"Sed non molestie ante.",
			"Pellentesque venenatis magna eu mauris tempor viverra.",
			"Ut venenatis magna mattis, tristique quam quis, aliquet ante. Curabitur aliquam sed nibh facilisis imperdiet.",
			"Vivamus sed ante dignissim, scelerisque arcu vel, finibus ipsum.",
			"In tempor sagittis augue vel lacinia. Aliquam imperdiet ipsum quis turpis sodales, ut iaculis ligula convallis.",
			"Nam tempor porta mi mattis viverra. Cras convallis aliquet diam eget euismod.",
			"Curabitur id lacinia magna, eget euismod tortor. Aenean rhoncus metus at nisl posuere ullamcorper.",
			"Donec rutrum nisi at enim pharetra lobortis vel at elit. Nullam eu nisi semper ipsum commodo convallis quis eu ante.",
			"Nunc sapien elit, interdum vitae urna et, aliquam luctus diam. Mauris rutrum vitae lorem quis rutrum.",
			"Interdum et malesuada fames ac ante ipsum primis in faucibus.",
			"Aliquam auctor magna eget dui commodo, ac cursus lectus feugiat.",
			"Morbi finibus consequat nibh eget placerat. Pellentesque sagittis sit amet turpis eget congue.",
			"Integer varius consequat efficitur. Sed gravida vestibulum ultricies.",
			"Duis facilisis, massa in aliquet elementum, ante tortor facilisis arcu, ut gravida augue metus sed nisl.",
			"Mauris et urna id lacus interdum congue. Pellentesque et viverra leo.",
			"Proin tempus semper laoreet. Cras lacinia lectus non metus rhoncus, sed fringilla justo tincidunt.",
			"Maecenas quis sapien sit amet augue facilisis sollicitudin. Suspendisse potenti.",
			"Aliquam at ex vitae est lobortis vestibulum vel sed ante. In condimentum est quis congue ornare.",
			"Nam ut ultricies justo. Pellentesque imperdiet blandit sodales. Suspendisse quis lacinia leo.",
			"Suspendisse commodo, nisl sed euismod congue, orci purus elementum urna, at commodo diam sem ac ante.",
			"Nullam gravida enim non nibh varius, sed vestibulum elit gravida.",
			"Praesent pretium urna tortor, vel sagittis mauris volutpat sit amet.",
			"Maecenas magna diam, suscipit ac blandit et, vulputate vel augue.",
			"Nullam nunc odio, aliquam vitae enim eget, posuere suscipit eros.",
			"Vivamus quis maximus diam, a facilisis dui. Pellentesque eu nisl blandit, gravida tellus vitae, mollis dolor.",
			"Sed vel mollis nisl. Sed sollicitudin orci urna. Sed consequat, dolor a eleifend euismod, est diam fringilla ex, ac dictum magna dui id sapien.",
			"In dapibus elit ipsum, non sollicitudin metus rutrum efficitur. Phasellus tincidunt, velit nec tincidunt mattis, felis odio dictum ante, eget facilisis leo erat vel est.",
			"Praesent luctus eleifend nulla. Vestibulum nibh eros, feugiat id pulvinar ut, tristique ut purus.",
			"Nam vel gravida sem. Suspendisse consequat augue urna, sed tempus massa suscipit vitae.",
			"Pellentesque efficitur, ipsum at bibendum cursus, arcu tortor pellentesque est, id eleifend lorem augue vel mauris.",
			"Sed in tincidunt purus. Sed commodo pharetra libero, nec rhoncus turpis mollis vel.",
			"Fusce sed massa malesuada dolor convallis eleifend vel ac nisi. Morbi bibendum nisi sed turpis porta, eget condimentum massa tristique.",
			"Mauris dictum dui sit amet felis pulvinar, sit amet ornare velit pretium.",
			"Quisque ac ante eros. Pellentesque dapibus vestibulum eros, nec pharetra neque vulputate gravida.",
			"Ut vitae augue scelerisque orci lacinia bibendum sed vitae augue.",
			"Mauris placerat bibendum dui, id varius sapien viverra a. Sed lectus odio, placerat et blandit mattis, viverra sit amet metus.",
		};
			public static string GetText(int rowIndex, int seed = 42) {
				if(rowIndex <= CommonEntriesCount)
					return (rowIndex == 0) ? Explanation : StaticGenerated[rowIndex - 1];
				return StaticGenerated[(Utils.HashCodeHelper.Finish(seed, rowIndex) & int.MaxValue) % StaticGenerated.Length];
			}
		}
		public const int MessagesGeneratorInterval = 45;
		partial class Channel {
			sealed class TimeoutState<TEvent> where TEvent : ContactEvent {
				readonly Dictionary<long, int> state;
				readonly Func<long, TEvent> createEventDefault;
				public TimeoutState(long[] ids, List<ContactEvent> events, Func<long, TEvent> createEvent) {
					this.createEventDefault = createEvent;
					this.state = Initialize(ids, events);
				}
				public int Count {
					get { return state.Count; }
				}
				Dictionary<long, int> Initialize(long[] ids, List<ContactEvent> events) {
					var state = new Dictionary<long, int>();
					var stateIds = DataGenerator.GetRandomIds(ids);
					foreach(long id in ids) {
						if(stateIds.Contains(id))
							state.Add(id, DataGenerator.GetCount(MessagesGeneratorInterval / 2, MessagesGeneratorInterval * 2));
						else AddEvent(id, events, createEventDefault);
					}
					return state;
				}
				public void Ensure(List<ContactEvent> events, Func<long, TEvent> createEvent = null) {
					var stateIds = state.Keys.ToArray();
					var durations = state.Values.ToArray();
					for(int i = 0; i < durations.Length; i++) {
						long id = stateIds[i];
						int duration = durations[i];
						if(--duration < 1) {
							AddEvent(id, events, createEvent);
							state.Remove(id);
						}
						else state[id] = duration;
					}
				}
				public void Update(long[] ids, List<ContactEvent> events, Func<long, TEvent> createEvent = null) {
					var stateIds = DataGenerator.GetRandomIds(ids);
					foreach(long id in stateIds) {
						int duration;
						if(!state.TryGetValue(id, out duration)) {
							state.Add(id, DataGenerator.GetCount(MessagesGeneratorInterval / 2, MessagesGeneratorInterval * 2));
							AddEvent(id, events, createEvent);
						}
						else state[id] = duration + DataGenerator.GetCount(0, MessagesGeneratorInterval);
					}
				}
				void AddEvent(long id, List<ContactEvent> events, Func<long, TEvent> createEvent) {
					var @event = (createEvent ?? createEventDefault)?.Invoke(id);
					if(@event != null) events.Add(@event);
				}
			}
			static class MessagesDataGenerator {
				public static void Weather(long you, long other, Action<long, string, DateTime> onMessage) {
					var firstSent = DateTime.Now.AddDays(-1);
					onMessage?.Invoke(other, "Hi {0},", firstSent.AddHours(10));
					onMessage?.Invoke(other, "I hope you are doing well!", firstSent.AddHours(10));
					onMessage?.Invoke(you, "Hi {1}! I was just about to message you! I ran into Stephen earlies today.", firstSent.AddHours(10.1));
					onMessage?.Invoke(other, " What did he say? Did he mention the meeting tomorrow?", firstSent.AddHours(10.2));
					onMessage?.Invoke(you, "Yes, but he didn’t mention specifics. Something about changing the venue for next Saturday", firstSent.AddHours(10.9));
					onMessage?.Invoke(you, "I suppose it wasn’t available", firstSent.AddHours(10.92));
					onMessage?.Invoke(other, "They mentioned there was an issue with their reservation system and already assigned it to another client", firstSent.AddHours(12));
					onMessage?.Invoke(you, "That is a shame! I really liked the location, and their facilities weren’t half bad either!", firstSent.AddHours(12.02));
					onMessage?.Invoke(other, "Do you know if that place Sam spoke about is still available?", firstSent.AddHours(12.09));
					onMessage?.Invoke(other, "I remember he said it isn’t too far away", firstSent.AddHours(12.10));
					onMessage?.Invoke(you, "I’ll give him a call later, but as I recall, they only had a small conference area", firstSent.AddHours(12.5));
					onMessage?.Invoke(other, "I’m sure we can make it work!", firstSent.AddHours(13));
					onMessage?.Invoke(other, "Perhaps they’ll allow us to leave the doors open and some people can sit outside", firstSent.AddHours(13.05));
					onMessage?.Invoke(you, "I just hope the weather holds up!", firstSent.AddHours(13.4));
					onMessage?.Invoke(you, "It usually rains a lot this time of the year", firstSent.AddHours(13.45));
				}
				public static void Expedition(long you, long other, Action<long, string, DateTime> onMessage) {
					var firstSent = DateTime.Now.AddHours(-6);
					onMessage?.Invoke(other, "Hi {0}", firstSent.AddHours(5));
					onMessage?.Invoke(you, "Hi", firstSent.AddHours(5.1));
					onMessage?.Invoke(other, "I hope you don’t mind that I contacted you directly", firstSent.AddHours(5.1));
					onMessage?.Invoke(other, "Grace mentioned you are an expert and might be able to help me", firstSent.AddHours(5.2));
					onMessage?.Invoke(you, "I don’t mind at all! I don’t know of I’d call myself an ‘expert’, but I’d be happy to help", firstSent.AddHours(5.22));
					onMessage?.Invoke(other, "We are arranging an expedition to Denali", firstSent.AddHours(5.23));
					onMessage?.Invoke(other, "We are planning to take a group of about 15 climbers with varying degrees of experience", firstSent.AddHours(5.4));
					onMessage?.Invoke(you, "When do you plan to go?", firstSent.AddHours(5.46));
					onMessage?.Invoke(other, "I arranged accommodation from the 15th to give us time to acclimatize and make other preparations", firstSent.AddHours(5.5));
					onMessage?.Invoke(you, "Unfortunately, I’m already leaving on another trip on the 10th", firstSent.AddHours(5.53));
					onMessage?.Invoke(you, "but I can send you the contact details of my colleague and fellow mountaineer", firstSent.AddHours(5.55));
					onMessage?.Invoke(other, "That’ll be great!", firstSent.AddHours(5.6));
					onMessage?.Invoke(other, "Does he have a lot of experience? Especially with groups", firstSent.AddHours(5.61));
				}
				public static void CV(long you, long other, Action<long, string, DateTime> onMessage) {
					var firstSent = DateTime.Now.AddHours(-12);
					onMessage?.Invoke(you, "Thanks again for your time this morning. It was great to hear more about your experience", firstSent.AddHours(10));
					onMessage?.Invoke(you, "As mentioned, I’m excited to introduce you to the Head of Marketing so you can discuss the role in greater detail. Let’s catch up again on Friday and we can address outstanding issues at that time.", firstSent.AddHours(10.1));
					onMessage?.Invoke(other, "Hi {0}, it was great talking to you today. I'm available at 15:00 on Friday to continue our discussion", firstSent.AddHours(11.2));
					onMessage?.Invoke(you, "Can you also send me copy of your CV? There is an attached consent form at the bottom, so if you could also complete this, that would be fantastic", firstSent.AddHours(11.23));
					onMessage?.Invoke(other, "I’ll complete it and send you a copy of my CV before the end of the day. Do you need anything else?", firstSent.AddHours(11.25));
					onMessage?.Invoke(you, "Not at the moment. I can send you a briefing document and job description for the company and the role if you’d like?", firstSent.AddHours(11.35));
					onMessage?.Invoke(other, "Sure! More information is always helpful", firstSent.AddHours(11.36));
					onMessage?.Invoke(other, "What is the name of the Head of Marketing? It would be nice to know who I’m going to work with", firstSent.AddHours(11.37));
					onMessage?.Invoke(you, "His name is James Carrington. He recently joined the team, so he’ll be able to tell you about his experience as a newcomer in this company", firstSent.AddHours(11.5));
					onMessage?.Invoke(other, "That’s good. I have a lot of questions I’d like to ask him, and it would be great to get his initial impression of the company", firstSent.AddHours(11.52));
					onMessage?.Invoke(you, "If you’d like, I can set up an informal meeting with him (and his team) next week and you can get to know the people you might be working with in the near future", firstSent.AddHours(11.6));
					onMessage?.Invoke(other, "I’m away on business next week, perhaps we can arrange a meeting for next Monday?", firstSent.AddHours(11.7));
					onMessage?.Invoke(you, "I’ll give James a call later today and find out if he is available next Monday?", firstSent.AddHours(11.8));
					onMessage?.Invoke(you, "In the meantime, you can have a look at his profile on the company’s website", firstSent.AddHours(11.85));
					onMessage?.Invoke(other, "I’ll have a look. Please let me know about the meeting", firstSent.AddHours(11.85));
					onMessage?.Invoke(you, "Will do!", firstSent.AddHours(11.85));
				}
				public static int LoremIpsum(long you, long other, Action<long, string, DateTime> onMessage) {
					var ids = DataGenerator.GetRandomIds(DevAVEmpployeesInMemoryServer.LoremIpsum.StaticGenerated.Length);
					int counter = 0;
					var firstSent = DateTime.Now.AddMinutes(-ids.Count * MessagesGeneratorInterval);
					int readCount = DataGenerator.GetCount(ids.Count - 10, ids.Count);
					foreach(long msgID in ids) {
						long id = (counter >= readCount) ? other : DataGenerator.EitherOr(you, other);
						var sent = firstSent.AddSeconds(DataGenerator.GetCount(0, MessagesGeneratorInterval) * (counter++));
						onMessage?.Invoke(id, DevAVEmpployeesInMemoryServer.LoremIpsum.GetText((int)msgID), sent);
					}
					return readCount;
				}
			}
		}
	}
}
