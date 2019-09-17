using Penguin.Cms.Logging.Entities;
using Penguin.Messaging.Core;
using Penguin.Persistence.Abstractions.Interfaces;
using Penguin.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Logging.Repositories
{
    /// <summary>
    /// An implementation of an IRepository for persisting logging messages
    /// </summary>
    public class LogEntryRepository : KeyedObjectRepository<LogEntry>
    {
        /// <summary>
        /// Constructs a new instance of this repository
        /// </summary>
        /// <param name="context">The backing persistence context to use for logged messages</param>
        /// <param name="messageBus">An optional message bus for sending out persistence messages</param>
        public LogEntryRepository(IPersistenceContext<LogEntry> context, MessageBus messageBus = null) : base(context, messageBus)
        {
        }

        /// <summary>
        /// Gets all messages logged by a given caller (Type.ToString())
        /// </summary>
        /// <param name="caller">The Type.ToString() for the object that did the logging</param>
        /// <returns>All messages requested by the given caller</returns>
        public List<LogEntry> GetByCaller(string caller) => this.Where(l => l.Caller == caller).ToList();

        /// <summary>
        /// Gets all messages logged as part of a single session
        /// </summary>
        /// <param name="session">The session id for the messages to be retrieved</param>
        /// <returns>All the messages logged as part of this session</returns>
        public List<LogEntry> GetBySession(string session) => this.Where(l => l.Session == session).ToList();

        /// <summary>
        /// Gets a list of all the distinct options for callers of the message logger
        /// To use as parameters in GetByCaller
        /// </summary>
        /// <returns>A list of all distinct callers of the message logger</returns>
        public List<string> GetDistinctCallers() => this.Select(c => c.Caller).Distinct().ToList();
    }
}