using System;
using System.Collections.Generic;

namespace DampGUI
{
    public class MockFriendServiceAgent : IFriendServiceAgent
    {
        public List<Friend> LoadFriends()
        {
            return null;
        }

        public List<Friend> FindFriends(string aName, Friends aFriends)
        {
            char[] name = aName.ToLower().ToCharArray();
            List<Friend> searchList = new List<Friend>();

            for (int i = 0; i < aFriends.TotalFriends; i++)
            {
                searchList.Add(aFriends.Get(i));
            }
            
            List<Friend> resultList = new List<Friend>();
            resultList.Clear();
            string strName;
            bool check = false;
            foreach (Friend friend in searchList)
            {
                strName = friend.Name.ToLower();
                char[] FName = strName.ToCharArray();

                for (int i = 0; i <= (name.Length -1); i++)
                {
                    if (name.Length <= FName.Length || !(name.Length > FName.Length))
                    {
                        if (name[i] == FName[i])
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (check)
                {
                    resultList.Add(friend);
                    check = false;
                }
            }
            return resultList;
        }
    }
}
