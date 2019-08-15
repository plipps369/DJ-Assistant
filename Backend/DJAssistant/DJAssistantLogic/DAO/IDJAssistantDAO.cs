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
   
        DJItem GetDJItemByEmail(string email);

        #endregion

        #region Genre

        int AddGenreItem(GenreItem item);
 
        GenreItem GetGenreItemById(int genreId);

        GenreItem GetGenreItemByName(string name);

        List<GenreItem> GetGenreItems();

        #endregion

        #region Party

        int AddPartyItem(PartyItem item);

        bool DeletePartyItem(int partyId);

        PartyItem GetPartyItemById(int partyId);

        List<PartyItem> GetPartyItemsByDJId(int DJId);

        PartyItem GetPartyByName(string name);
        
        #endregion

        #region PartySong

        int AddPartySongItem(PartySongItem item);

        bool UpdatePartySongItem(PartySongItem item);
      
        PartySongItem GetPartySongItemById(int partySongId);

        List<PartySongItemWithDetails> GetPartySongItemWithDetailsByPartyId(int partyId);

        int GetTotalSongsRequestedByPartyId(int partyId);

        int GetTotalPlayedSongsByPartyId(int partyId);

        List<PartySongItemWithDetails> GetPartySongsPlayedByPartyName(string partyName);

        List<PartySongItemWithDetails> GetPartySongsNotPlayedByPartyName(string partyName);

        #endregion

        #region SongDJ

        void AddSongDJItem(SongDJItem item);

        bool DeleteSongDJItem(SongDJItem item);

        #endregion

        #region SongGenre

        void AddSongGenreItem(SongGenreItem item);

        bool DeleteSongGenreItem(SongGenreItem item);
        

        #endregion

        #region PartyGenre

        void AddPartyGenreItem(PartyGenreItem partyGenre);
        
        #endregion

        #region Song

        int AddSongItem(SongItem item);

        SongItem GetSongItemById(int songId);

        List<SongItem> GetSongsByPartyId(int partyID);

        List<SongItem> GetSongsByDJId(int dJId);

        List<SongItem> GetSongsByDJEmail(string email);

        #endregion

        #region Playlist

        int AddPlayList(PlaylistItem playlist);

        List<PlaylistItem> GetPlaylistItemsByDJId(int dJId);

        void AddSongPlayList(SongPlaylistItem songPlaylist);

        List<SongItem> GetSongItemsInPlayListByPlaylistId(int id);

        #endregion

    }
}
