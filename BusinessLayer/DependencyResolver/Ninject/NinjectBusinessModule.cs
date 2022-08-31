using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolver.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAboutDal>().To<EFAboutRepository>().InSingletonScope();

            Bind<IAdminDal>().To<EFAdminRepository>().InSingletonScope();

            Bind<IBlogDal>().To<EFBlogRepository>().InSingletonScope();

            Bind<ICategoryDal>().To<EFCategoryRepository>().InSingletonScope();

            Bind<ICommentDal>().To<EFCommentRepository>().InSingletonScope();

            Bind<IContactDal>().To<EFContactRepository>().InSingletonScope();

            Bind<IMessage2Dal>().To<EFMessage2Repository>().InSingletonScope();

            Bind<INewsLetterDal>().To<EFNewsLetterRepository>().InSingletonScope();

            Bind<INotificationDal>().To<EFNotificationRepository>().InSingletonScope();

            Bind<IWriterDal>().To<EFWriterRepository>().InSingletonScope();


            Bind<IAboutService>().To<AboutManager>().InSingletonScope();

            Bind<IAdminService>().To<AdminManager>().InSingletonScope();

            Bind<IBlogService>().To<BlogManager>().InSingletonScope();

            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();

            Bind<ICommentService>().To<CommentManager>().InSingletonScope();

            Bind<IContactService>().To<ContactManager>().InSingletonScope();

            Bind<IMessage2Service>().To<Message2Manager>().InSingletonScope();

            Bind<INewsLetterService>().To<NewsLetterManager>().InSingletonScope();

            Bind<INotificationService>().To<NotificationManager>().InSingletonScope();

            Bind<IWriterService>().To<WriterManager>().InSingletonScope();


        }
    }
}