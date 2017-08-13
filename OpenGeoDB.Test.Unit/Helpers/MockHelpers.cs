using System;
using Moq;
using OpenGeoDB.Services.Alert;
using OpenGeoDB.Test.Unit.Mocks;

namespace OpenGeoDB.Test.Unit.Helpers
{
    public static class MockHelpers
    {
        public static Mock<IAlertService> CreateAlertService()
        {
            var alertServiceMock = new Mock<IAlertService>(MockBehavior.Loose);

            alertServiceMock.Setup(mock => mock.Alert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action>()))
                            .Returns<string, string, string, Action>((title, message, actionTitle, action) => new DisposableActionMock(action, TimeSpan.FromMilliseconds(50)));

            return alertServiceMock;
        }
    }
}
