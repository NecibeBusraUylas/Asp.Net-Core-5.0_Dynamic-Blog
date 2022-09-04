using Autofac;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolver.Autofact
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFCategoryRepository>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<EFAboutRepository>().As<IAboutDal>().SingleInstance();

            builder.RegisterType<EFAdminRepository>().As<IAdminDal>().SingleInstance();

            builder.RegisterType<EFBlogRepository>().As<IBlogDal>().SingleInstance();

            builder.RegisterType<EFCommentRepository>().As<ICommentDal>().SingleInstance();

            builder.RegisterType<EFContactRepository>().As<IContactDal>().SingleInstance();

            builder.RegisterType<EFMessage2Repository>().As<IMessage2Dal>().SingleInstance();

            builder.RegisterType<EFNewsLetterRepository>().As<INewsLetterDal>().SingleInstance();

            builder.RegisterType<EFNotificationRepository>().As<INotificationDal>().SingleInstance();

            builder.RegisterType<EFWriterRepository>().As<IWriterDal>().SingleInstance();


            builder.RegisterType<AboutManager>().As<IAboutService>().SingleInstance();

            builder.RegisterType<AdminManager>().As<IAdminService>().SingleInstance();

            builder.RegisterType<BlogManager>().As<IBlogService>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();

            builder.RegisterType<ContactManager>().As<IContactService>().SingleInstance();

            builder.RegisterType<Message2Manager>().As<IMessage2Service>().SingleInstance();

            builder.RegisterType<NewsLetterManager>().As<INewsLetterService>().SingleInstance();

            builder.RegisterType<NotificationManager>().As<INotificationService>().SingleInstance();

            builder.RegisterType<WriterManager>().As<IWriterService>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        }
    }
}