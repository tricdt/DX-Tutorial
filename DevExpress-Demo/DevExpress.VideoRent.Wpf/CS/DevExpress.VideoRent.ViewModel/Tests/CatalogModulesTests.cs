#if DebugTest
using System;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
#if SL
using DevExpress.Xpf.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using TestClassAttribute = NUnit.Framework.TestFixtureAttribute;
using TestMethodAttribute = NUnit.Framework.TestAttribute;
using TestInitializeAttribute = NUnit.Framework.SetUpAttribute;
using TestCleanupAttribute = NUnit.Framework.TearDownAttribute;
using NUnit.Framework;
#endif

namespace DevExpress.VideoRent.ViewModel.Tests {
    [TestClass]
    public class MovieEditsTests : ViewModelTests {
        [TestMethod]
        public void CreateMoviesList() {
            using(MoviesList list = (MoviesList)ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false)) {
                MoviesListView moviesListView = MoviesListView.LastCreatedView;
                Assert.IsNotNull(moviesListView);
                Assert.IsNotNull(moviesListView.Module);
                Assert.AreEqual(list, moviesListView.Module);
            }
        }
        [TestMethod]
        public void CreateMovieDetail_CheckAllCountries() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView = MovieDetailView.LastCreatedView;
            Assert.IsNotNull(movieDetailView);
            Assert.IsNotNull(movieDetailView.Module);
            Assert.IsNotNull(movieDetailView.Module.MovieEdit.CountryEditData.List);
            movieDetailView.Module.Dispose();
        }
        [TestMethod]
        public void ChangeMovie_SaveAndDispose() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView = MovieDetailView.LastCreatedView;
            movieDetailView.Module.MovieEdit.VRObjectEditObject.VideoRentObject.Awards = "new Awards";
            movieDetailView.Module.SaveAndDispose();
            Assert.IsTrue(movieDetailView.Module.Disposed);
            Avatar.Reload();
            Assert.AreEqual("new Awards", Avatar.Awards);
        }
        [TestMethod]
        public void OpenSameMovieDetailTwice() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView1 = MovieDetailView.LastCreatedView;
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView2 = MovieDetailView.LastCreatedView;
            Assert.AreEqual(movieDetailView1, movieDetailView2);
            movieDetailView1.Module.Dispose();
        }
        [TestMethod]
        public void OpenDifferentMovieDetailTwice() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView1 = MovieDetailView.LastCreatedView;
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Cube.Oid), true);
            MovieDetailView movieDetailView2 = MovieDetailView.LastCreatedView;
            Assert.AreNotEqual(movieDetailView1, movieDetailView2);
            movieDetailView1.Module.Dispose();
            movieDetailView2.Module.Dispose();
        }
        [TestMethod]
        public void OpenMovieDetail_Close_OpenSecondTime() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView1 = MovieDetailView.LastCreatedView;
            movieDetailView1.Module.Close();
            ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true);
            MovieDetailView movieDetailView2 = MovieDetailView.LastCreatedView;
            Assert.AreNotEqual(movieDetailView1, movieDetailView2);
            movieDetailView1.Module.Dispose();
            movieDetailView2.Module.Dispose();
        }
        [TestMethod]
        public void ChangeMovieByDetail_CheckUpdateInMoviesList() {
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView = MoviesListView.LastCreatedView;
            moviesListView.Module.OpenDetail(Avatar.Oid);
            MovieDetailView detail = MovieDetailView.LastCreatedView;
            Movie avatar = new XPCollection<Movie>(moviesListView.Module.MoviesEdit.VRObjectsEditObject.VideoRentObjects, CriteriaOperator.Parse("Oid = ?", Avatar.Oid))[0];
            detail.Module.MovieEdit.VRObjectEditObject.VideoRentObject.Plot += "_Updated";
            string updatedPlot = detail.Module.MovieEdit.VRObjectEditObject.VideoRentObject.Plot;
            detail.Module.SaveAndDispose();
            Assert.AreEqual(updatedPlot, avatar.Plot);
            moviesListView.Module.Dispose();
        }
        [TestMethod]
        public void UnsubscribeEvents() {
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView1 = MoviesListView.LastCreatedView;
            moviesListView1.Module.Dispose();
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView2 = MoviesListView.LastCreatedView;
            moviesListView2.Module.OpenDetail(Avatar.Oid);
            MovieDetailView detail = MovieDetailView.LastCreatedView;
            Movie avatar = new XPCollection<Movie>(moviesListView2.Module.MoviesEdit.VRObjectsEditObject.VideoRentObjects, CriteriaOperator.Parse("Oid = ?", Avatar.Oid))[0];
            detail.Module.MovieEdit.VRObjectEditObject.VideoRentObject.Plot += "_Updated";
            detail.Module.SaveAndDispose();
            moviesListView2.Module.Dispose();
        }
        [TestMethod]
        public void AddMovie() {
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView = MoviesListView.LastCreatedView;
            moviesListView.Module.OpenDetail(null);
            MovieDetailView.LastCreatedView.Module.Dispose();
            moviesListView.Module.Dispose();
        }
        [TestMethod]
        public void CloseMovieDetailWithCloseAllDetails() {
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView = MoviesListView.LastCreatedView;
            moviesListView.Module.OpenDetail(Avatar.Oid);
            moviesListView.Module.OpenDetail(Nirvana.Oid);
            moviesListView.Module.CloseDetails();
            Assert.AreEqual(0, ModulesManager.Current.GetModulesForType(typeof(MovieDetailObject)).Count);
            moviesListView.Module.Dispose();
        }
        [TestMethod]
        public void CloseMovieDetailFromWithin() {
            ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false);
            MoviesListView moviesListView = MoviesListView.LastCreatedView;
            moviesListView.Module.OpenDetail(Avatar.Oid);
            MovieDetailView detail = MovieDetailView.LastCreatedView;
            detail.Module.Dispose();
            Assert.AreEqual(0, ModulesManager.Current.GetModulesForType(typeof(MovieDetailObject)).Count);
            moviesListView.Module.Dispose();
        }
        [TestMethod]
        public void DeleteMovieFormMoviesList_CheckIsSessionCommit() {
            using(MoviesList moviesList = (MoviesList)ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false)) {
                moviesList.MoviesEdit.CurrentRecord = SessionHelper.GetObject<Movie>(Avatar, moviesList.MoviesEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.IsTrue(moviesList.MoviesEdit.DeleteCurrentRecord());
                Assert.IsNull(moviesList.MoviesEdit.CurrentRecord);
                Assert.IsFalse(moviesList.VRObjectsListObject.Dirty);
            }
        }
        [TestMethod]
        public void ChangeTitle_Save_CheckModuleTitle() {
            using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                Assert.AreEqual("Avatar", movieDetail.Title);
                movieDetail.MovieEdit.VRObjectEditObject.VideoRentObject.Title = "Avatar new title";
                Assert.AreEqual("Avatar *", movieDetail.Title);
                Assert.IsTrue(movieDetail.Save());
                Assert.AreEqual("Avatar new title", movieDetail.Title);
            }
        }
        [TestMethod]
        public void ListIsSorted() {
            using(MoviesList moviesList = (MoviesList)ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false)) {
                Assert.AreNotEqual(0, moviesList.VRObjectsListObject.MoviesEditObject.VideoRentObjects.Sorting.Count);
            }
        }
        [TestMethod]
        public void IntialCurrentRecordIsSet() {
            using(MoviesList moviesList = (MoviesList)ModulesManager.Current.OpenModuleObjectDetail(new MoviesListObject(Session), false)) {
                Assert.IsNotNull(moviesList.MoviesEdit.CurrentRecord);
            }
        }
        [TestMethod]
        public void AddArtist_CheckEdits() {
            using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                MovieAddArtistEdit addArtistEdit = movieDetail.AddArtist();
                addArtistEdit.AddVRObjectEditObject.VideoRentObject.Artist = SessionHelper.GetObject<Artist>(JamesCameron, addArtistEdit.AddVRObjectEditObject.VideoRentObject.Session);
                addArtistEdit.AddVRObjectEditObject.VideoRentObject.Line = MovieArtistLine.FromName(addArtistEdit.AddVRObjectEditObject.VideoRentObject.Session, "Actor");
                Assert.IsTrue(addArtistEdit.SaveAndDispose());
                Assert.IsNull(movieDetail.MovieAddArtistEdit);
            }
        }
        [TestMethod]
        public void AddMovieCategoryWithExistingName() {
            string categoryName = new XPCollection<MovieCategory>(Session)[0].Name;
            using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                using(MovieCategoryDetail movieCategoryDetail = movieDetail.MovieEdit.OpenNewCategoryDialog()) {
                    movieCategoryDetail.MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject.Name = categoryName;
                    movieCategoryDetail.SaveAndDispose();
                }
            }
        }
        [TestMethod]
        public void AddCategory_Save() {
            using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                using(MovieCategoryDetail movieCategoryDetail = movieDetail.MovieEdit.OpenNewCategoryDialog()) {
                    movieCategoryDetail.MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject.Name = "NewCategoryName";
                    movieCategoryDetail.SaveAndDispose();
                }
                Assert.AreEqual("NewCategoryName", movieDetail.MovieEdit.VRObjectEditObject.VideoRentObject.Category.Name);
                movieDetail.Save();
            }
        }
    }
    [TestClass]
    public class ArtistEditsTests : ViewModelTests {
        [TestMethod]
        public void CreateArtistsList() {
            using(ArtistsList list = (ArtistsList)ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false)) {
                ArtistsListView artistsListView = ArtistsListView.LastCreatedView;
                Assert.IsNotNull(artistsListView);
                Assert.IsNotNull(artistsListView.Module);
                Assert.AreEqual(list, artistsListView.Module);
            }
        }
        [TestMethod]
        public void ChangeArtistByDetail_CheckUpdateInArtistsList() {
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView = ArtistsListView.LastCreatedView;
            artistsListView.Module.OpenDetail(JamesCameron.Oid);
            ArtistDetailView detail = ArtistDetailView.LastCreatedView;
            Artist jamesCameron = new XPCollection<Artist>(artistsListView.Module.ArtistsEdit.VRObjectsEditObject.VideoRentObjects, CriteriaOperator.Parse("Oid = ?", JamesCameron.Oid))[0];
            detail.Module.ArtistEdit.VRObjectEditObject.VideoRentObject.Biography += "_Updated";
            string updatedBiography = detail.Module.ArtistEdit.VRObjectEditObject.VideoRentObject.Biography;
            detail.Module.SaveAndDispose();
            Assert.AreEqual(updatedBiography, jamesCameron.Biography);
            artistsListView.Module.Dispose();
        }
        [TestMethod]
        public void UnsubscribeEvents() {
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView1 = ArtistsListView.LastCreatedView;
            artistsListView1.Module.Dispose();
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView2 = ArtistsListView.LastCreatedView;
            artistsListView2.Module.OpenDetail(JamesCameron.Oid);
            ArtistDetailView detail = ArtistDetailView.LastCreatedView;
            Artist jamesCameron = new XPCollection<Artist>(artistsListView2.Module.ArtistsEdit.VRObjectsEditObject.VideoRentObjects, CriteriaOperator.Parse("FullName = ?", JamesCameron.FullName))[0];
            detail.Module.ArtistEdit.VRObjectEditObject.VideoRentObject.Biography += "_Updated";
            detail.Module.SaveAndDispose();
            artistsListView2.Module.Dispose();
        }
        [TestMethod]
        public void AddArtist() {
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView = ArtistsListView.LastCreatedView;
            artistsListView.Module.OpenDetail(null);
            ArtistDetailView.LastCreatedView.Module.Dispose();
            artistsListView.Module.Dispose();
        }
        [TestMethod]
        public void CloseArtistDetailWithCloseAllDetails() {
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView = ArtistsListView.LastCreatedView;
            artistsListView.Module.OpenDetail(JamesCameron.Oid);
            artistsListView.Module.OpenDetail(UweBoll.Oid);
            artistsListView.Module.CloseDetails();
            Assert.AreEqual(0, ModulesManager.Current.GetModulesForType(typeof(ArtistDetailObject)).Count);
            artistsListView.Module.Dispose();
        }
        [TestMethod]
        public void CloseArtistDetailFromWithin() {
            ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false);
            ArtistsListView artistsListView = ArtistsListView.LastCreatedView;
            artistsListView.Module.OpenDetail(JamesCameron.Oid);
            ArtistDetailView detail = ArtistDetailView.LastCreatedView;
            detail.Module.Dispose();
            Assert.AreEqual(0, ModulesManager.Current.GetModulesForType(typeof(ArtistDetailObject)).Count);
            artistsListView.Module.Dispose();
        }
        [TestMethod]
        public void CreateArtistDetail_Save_Dispose_CheckUnsubscribeFromSetUpdated() {
            using(ArtistDetail artistDetail = (ArtistDetail)ModulesManager.Current.OpenModuleObjectDetail(new ArtistDetailObject(Session, JamesCameron.Oid), true)) {
                artistDetail.ArtistEdit.VRObjectEditObject.VideoRentObject.FirstName = "J";
                Assert.IsTrue(artistDetail.Save());
            }
            Assert.IsNull(AllObjects<Artist>.Set.GetUpdatedEvent());
            Assert.IsNull(AllObjects<Movie>.Set.GetUpdatedEvent());
        }
        [TestMethod]
        public void ChangeName_Save_CheckModuleTitle() {
            using(ArtistDetail artistDetail = (ArtistDetail)ModulesManager.Current.OpenModuleObjectDetail(new ArtistDetailObject(Session, JamesCameron.Oid), true)) {
                Assert.AreEqual("James Cameron", artistDetail.Title);
                artistDetail.ArtistEdit.VRObjectEditObject.VideoRentObject.FirstName = "J";
                Assert.AreEqual("James Cameron *", artistDetail.Title);
                Assert.IsTrue(artistDetail.Save());
                Assert.AreEqual("J Cameron", artistDetail.Title);
            }
        }
        [TestMethod]
        public void ListIsSorted() {
            using(ArtistsList artistslist = (ArtistsList)ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false)) {
                Assert.AreNotEqual(0, artistslist.VRObjectsListObject.ArtistsEditObject.VideoRentObjects.Sorting.Count);
            }
        }
        [TestMethod]
        public void AddPhoto_CheckDirtyRough() {
            Image picture = new Bitmap(2, 2);
            TestOpenFileDialogView.OpenImageDelegate = () => { return picture; };
            using(ArtistsList artistslist = (ArtistsList)ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false)) {
                Artist artist = SessionHelper.GetObject<Artist>(JamesCameron, artistslist.ArtistsEdit.VRObjectsEditObject.VideoRentObjects.Session);
                artistslist.ArtistsEdit.CurrentRecord = artist;
                artistslist.ArtistPicturesEdit.AddPicture();
                Assert.AreEqual(1, artist.Pictures.Count);
                Assert.AreEqual(picture, artist.Pictures[0].Image);
                Assert.IsTrue(artistslist.DirtyRough);
            }
        }
        [TestMethod]
        public void AddPhoto_Refresh() {
            using(ArtistsList artistslist = (ArtistsList)ModulesManager.Current.OpenModuleObjectDetail(new ArtistsListObject(Session), false)) {
                Artist artist = SessionHelper.GetObject<Artist>(JamesCameron, artistslist.ArtistsEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.IsNotNull(artist.Pictures);
                Assert.AreEqual(0, artist.Pictures.Count);
                artist.AddPicture(new Bitmap(2, 2));
                Assert.IsNotNull(artist.Pictures);
                Assert.AreNotEqual(0, artist.Pictures.Count);
                artistslist.VRObjectsListObject.Reload();
                artist = SessionHelper.GetObject<Artist>(JamesCameron, artistslist.ArtistsEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.IsNotNull(artist.Pictures);
                Assert.AreNotEqual(0, artist.Pictures.Count);
            }
        }
    }
    [TestClass]
    public class MovieCategoryEditsTests : ViewModelTests {
        [TestMethod]
        public void CreateMovieCategoriesList() {
            using(MovieCategoriesList list = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                MovieCategoriesListView movieCategoriesListView = MovieCategoriesListView.LastCreatedView;
                Assert.IsNotNull(movieCategoriesListView);
                Assert.IsNotNull(movieCategoriesListView.Module);
                Assert.AreEqual(list, movieCategoriesListView.Module);
            }
        }
        [TestMethod]
        public void CreateMovieCategoriesListAddCategorySave() {
            using(MovieCategoriesList list = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                MovieCategoriesListView movieCategoriesListView = MovieCategoriesListView.LastCreatedView;
                MovieCategoryEdit categoryEdit = list.MovieCategoryEdit;
                list.CreateCategory();
                Assert.AreNotEqual(categoryEdit, list.MovieCategoryEdit);
                list.Close();
            }
        }
        [TestMethod]
        public void CreateMovieCategoriesList_ChangeCurrentRecord_CheckCategoryEdit() {
            MovieCategory category1 = new MovieCategory(Session, "category1");
            SessionHelper.CommitSession(Session, null);
            using(MovieCategoriesList list = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                MovieCategoryEdit categoryEdit = list.MovieCategoryEdit;
                Assert.IsTrue(2 >= list.MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects.Count);
                Assert.AreEqual(list.MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects[0], list.MovieCategoriesEdit.CurrentRecord);
                categoryEdit.MovieCategoryEditObject.VideoRentObject.Prices[0].Days2RentPrice += 2M;
                list.Save();
                list.MovieCategoriesEdit.CurrentRecord = list.MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects[1];
                Assert.AreNotEqual(categoryEdit, list.MovieCategoryEdit);
            }
        }
        [TestMethod]
        public void AddCategory() {
            ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoryDetailObject(Session, null), false);
            MovieCategoryDetailView.LastCreatedView.Module.Dispose();
        }
        [TestMethod]
        public void ListIsSorted() {
            using(MovieCategoriesList categorieslist = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                Assert.AreNotEqual(0, categorieslist.VRObjectsListObject.MovieCategoriesEditObject.VideoRentObjects.Sorting.Count);
            }
        }
        [TestMethod]
        public void ChangeCategoryName_EditMovie_CheckCategoryName_AddMovieCategoryWithExistingName() {
            string categoryName = new XPCollection<MovieCategory>(Session)[0].Name;
            Guid categoryOid = new MovieCategory(Session).Oid;
            SessionHelper.CommitSession(Session, null);
            using(MovieCategoriesList categorieslist = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                categorieslist.MovieCategoriesEdit.CurrentRecord = SessionHelper.GetObjectByKey<MovieCategory>(categoryOid, categorieslist.MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.AreEqual(categorieslist.MovieCategoriesEdit.CurrentRecord, categorieslist.MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject);
                categorieslist.MovieCategoriesEdit.CurrentRecord.Name = "new name";
                Assert.IsTrue(categorieslist.DirtyRough);
                using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                    XPCollection<MovieCategory> categories = new XPCollection<MovieCategory>(movieDetail.MovieEdit.MovieCategoryEditData.List, CriteriaOperator.Parse("Name = ?", "new name"));
                    Assert.AreEqual(1, categories.Count);
                }
            }
            using(MovieDetail movieDetail = (MovieDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(Session, Avatar.Oid), true)) {
                using(MovieCategoryDetail movieCategoryDetail = movieDetail.MovieEdit.OpenNewCategoryDialog()) {
                    movieCategoryDetail.MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject.Name = categoryName;
                    movieCategoryDetail.SaveAndDispose();
                }
            }
        }
        [TestMethod]
        public void OpenCategoriesList_Dispose_SendSetUpdated() {
            Guid categoryOid = new MovieCategory(Session).Oid;
            SessionHelper.CommitSession(Session, null);
            MovieCategoriesList categorieslist = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false);
            categorieslist.Dispose();
            AllObjects<MovieCategory>.Set.RaiseUpdated(new TestEditableObject(categoryOid));
        }
        [TestMethod]
        public void CheckAnotherCategoriesList() {
            using(MovieCategoriesList categorieslist = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                MovieCategory editCategory = categorieslist.MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject;
                Assert.IsTrue(categorieslist.MovieCategoryEdit.AnotherCategories.List.IndexOf(editCategory) < 0);
            }
        }
        [TestMethod]
        public void AddTwoCategories_DeleteThem() {
            using(MovieCategoriesList list = (MovieCategoriesList)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoriesListObject(Session), false)) {
                Guid firstOne = list.CreateCategory().Oid;
                list.CreateCategory();
                Assert.IsTrue(list.MovieCategoriesEdit.DeleteCurrentRecord());
                list.MovieCategoriesEdit.CurrentRecord = SessionHelper.GetObjectByKey<MovieCategory>(firstOne, list.MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects.Session);
                Assert.IsTrue(list.MovieCategoriesEdit.DeleteCurrentRecord());
                list.Close();
            }
        }
    }
    [TestClass]
    public class CompanyEditsTests : ViewModelTests {
        [TestMethod]
        public void CreateCompaniesList() {
            using(CompaniesList list = (CompaniesList)ModulesManager.Current.OpenModuleObjectDetail(new CompaniesListObject(Session), false)) {
                CompaniesListView companiesListView = CompaniesListView.LastCreatedView;
                Assert.IsNotNull(companiesListView);
                Assert.IsNotNull(companiesListView.Module);
                Assert.AreEqual(list, companiesListView.Module);
            }
        }
        [TestMethod]
        public void ListIsSorted() {
            using(CompaniesList companieslist = (CompaniesList)ModulesManager.Current.OpenModuleObjectDetail(new CompaniesListObject(Session), false)) {
                Assert.AreNotEqual(0, companieslist.VRObjectsListObject.CompaniesEditObject.VideoRentObjects.Sorting.Count);
            }
        }
        [TestMethod]
        public void ChangeCompanyByList_OpenDetail_CheckChanges() {
            using(CompaniesList list = (CompaniesList)ModulesManager.Current.OpenModuleObjectDetail(new CompaniesListObject(Session), false)) {
                Company company = SessionHelper.GetObject<Company>(FoxCompany, list.CompaniesEdit.VRObjectsEditObject.VideoRentObjects.Session);
                company.Tag = "changed tag";
                using(CompanyDetail detail = (CompanyDetail)list.OpenDetail(company.Oid)) {
                    Assert.AreEqual("changed tag", detail.CompanyEdit.VRObjectEditObject.VideoRentObject.Tag);
                }
            }
        }
    }

}
#endif
