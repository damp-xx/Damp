/**
 * @file   	IServerCommand.cs
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file defines the interface for the IServerCommand
 * @section	LICENSE GPL 
 */

namespace DampServer.interfaces
{
    public interface IServerCommand
    {
        /**
         * @brief Indicates if the command needs to be authcenticated
         */
        bool NeedsAuthcatication { get; }

        /**
         * @brief Indicates if the command needs is persistant
         */
        bool IsPersistant { get; }

        /**
         * @brief functions that tells the request processor what command the class handles
         */
        bool CanHandleCommand(string cmd);

        /**
         * @brief the execute method from GoF Command pattern
         */
        void Execute(ICommandArgument http, string cmd = null);
    }
}