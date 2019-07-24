using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Server.Factories
{
    [TestFixture]
    public class LabelFactoryTests
    {
        public LabelFactory LabelFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            LabelFactory = new LabelFactory();
        }

        [Test]
        public void LabelFactory_CreateEntity_Project()
        {
            // arrange
            var project = GetProject();
            var key = "example_key";

            // act
            var result = LabelFactory.CreateEntity(key, project);

            // assert
            result.Key.ShouldBe(key);
            result.Name.ShouldBe(key);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.ProjectUid.ShouldBe(project.Uid);
            result.ProjectId.ShouldBe(project.Id);
            result.ProjectName.ShouldBe(project.Name);
        }

        [Test]
        public void LabelFactory_CreateEntityFromRequest_LabelEditRequest_Label()
        {
            // arrange
            var label = GetLabel();
            var request = GetLabelEditRequest(label);

            // act
            var result = LabelFactory.CreateEntityFromRequest(request, label);

            // assert
            result.UpdatedBy.ShouldBe(request.CurrentUserId);
            result.Key.ShouldBe(request.LabelKey);
            result.Name.ShouldBe(request.LabelKey);

            result.Description.ShouldBe(request.Description);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void LabelFactory_CreateEntityFromRequest_LabelCloneRequest_Label()
        {
            // arrange
            var label = GetLabel();
            var request = GetLabelCloneRequest(label);

            // act
            var result = LabelFactory.CreateEntityFromRequest(request, label);

            // assert
            result.OrganizationId.ShouldBe(label.OrganizationId);
            result.OrganizationUid.ShouldBe(label.OrganizationUid);
            result.OrganizationName.ShouldBe(label.OrganizationName);

            result.ProjectUid.ShouldBe(label.Uid);
            result.ProjectId.ShouldBe(label.Id);
            result.ProjectName.ShouldBe(label.Name);

            result.Key.ShouldBe(request.LabelKey);
            result.Name.ShouldBe(request.LabelKey);
            result.Description.ShouldBe(request.Description);

            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void LabelFactory_CreateEntityFromRequest_LabelCreateRequest_Project()
        {
            // arrange
            var project = GetProject();
            var label = GetLabel();
            var request = GetLabelCreateRequest(label, project);

            // act
            var result = LabelFactory.CreateEntityFromRequest(request, project);

            // assert
            result.Key.ShouldBe(request.LabelKey);
            result.Name.ShouldBe(request.LabelKey);
            result.Description.ShouldBe(request.Description);

            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.ProjectUid.ShouldBe(project.Uid);
            result.ProjectId.ShouldBe(project.Id);
            result.ProjectName.ShouldBe(project.Name);

            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void LabelFactory_CreateDtoFromEntity_Label()
        {
            // arrange
            var label = GetLabel();

            // act
            var result = LabelFactory.CreateDtoFromEntity(label);

            // assert
            result.OrganizationUid.ShouldBe(label.OrganizationUid);
            result.OrganizationName.ShouldBe(label.OrganizationName);
            result.Uid.ShouldBe(label.Uid);
            result.Name.ShouldBe(label.Name);
            result.IsActive.ShouldBe(label.IsActive);

            result.Uid.ShouldBe(label.Uid);
            result.CreatedAt.ShouldBe(label.CreatedAt);
            result.UpdatedAt.ShouldBe(label.UpdatedAt);
            result.Key.ShouldBe(label.Key);
            result.Name.ShouldBe(label.Name);
            result.Description.ShouldBe(label.Description);
            result.LabelTranslationCount.ShouldBe(label.LabelTranslationCount);

            result.CreatedAt.ShouldNotBeNull();
        }

        [Test]
        public void LabelFactory_UpdateEntityForChangeActivation()
        {
            // arrange
            var label = GetLabel();
            var isActive = label.IsActive;

            // act
            var result = LabelFactory.UpdateEntityForChangeActivation(label);

            // assert

            result.IsActive.ShouldBe(!isActive);
        }

    }
}