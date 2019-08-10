using DJAssistantLogic.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.DAO
{
    public interface IDJAssistantDAO
    {
        #region DJ

        int AddDJItem(DJItem item);
        bool UpdateDJItem(DJItem item);
        bool DeleteDJItem(int DJId);
        DJItem GetDJItemById(int DJId);
        DJItem GetDJItemByEmail(string email);

        #endregion

        #region Genre

        int AddGenreItem(GenreItem item);
        bool UpdateGenreItem(GenreItem item);
        bool DeleteGenreItem(int genreId);
        GenreItem GetGenreItemById(int genreId);
        GenreItem GetGenreItemByName(string name);

        #endregion

        #region Party

        int AddPartyItem(PartyItem item);
        bool UpdatePartyItem(PartyItem item);
        bool DeletePartyItem(int partyId);
        PartyItem GetPartyItemById(int partyId);
        PartyItem GetPartyByName(string name);
        List<PartyItem> GetPartyItemsByDJId(int DJId);

        #endregion

        #region PartySong

        int AddPartySongItem(PartySongItem item);
        bool UpdatePartySongItem(PartySongItem item);
        bool DeletePartySongItem(int partySongId);
        PartySongItem GetPartySongItemById(int partySongId);
        List<PartySongItem> GetPartySongItemByPartyId(int partyId);

        #endregion

        #region SongDJ

        void AddSongDJItem(SongDJItem item);
        bool DeleteSongDJItem(SongDJItem item);

        #endregion

        #region SongGenre

        void AddSongGenreItem(SongGenreItem item);
        bool DeleteSongGenreItem(SongGenreItem item);
        List<GenreItem> GetGenreItems();

        #endregion

        #region Song

        int AddSongItem(SongItem item);
        bool UpdateSongItem(SongItem item);
        bool DeleteSongItem(int songId);
        SongItem GetSongItemById(int songId);

        #endregion
    }
}
