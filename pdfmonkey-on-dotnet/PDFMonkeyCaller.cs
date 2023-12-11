namespace PDFMonkeyOnDotnet
{
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    using Entities;
    using Helpers;

    public class PDFMonkeyCaller
    {
        private string apiSecretKey;

        public PDFMonkeyCaller(string apiSecret)
        {
            this.apiSecretKey = apiSecret;
        }

        public async Task<User?> GetUser()
        {
            string path = $"https://api.pdfmonkey.io/api/v1/current_user";

            var caller = new Caller<User>(this.apiSecretKey);
            var documentCard = await caller.Get(path, User.Identifier);

            return documentCard;
        }

        public async Task<DocumentCard> GetDocumentCard(Guid documentId)
        {
            string path = $"https://api.pdfmonkey.io/api/v1/document_cards/{documentId}";

            var caller = new Caller<DocumentCard>(this.apiSecretKey);
            var documentCard = await caller.Get(path, DocumentCard.Identifier);

            return documentCard;
        }

        public async Task<Document> GetDocument(Guid documentId)
        {
            string path = $"https://api.pdfmonkey.io/api/v1/documents/{documentId}";

            var caller = new Caller<Document>(this.apiSecretKey);
            var document = await caller.Get(path, Document.Identifier);

            return document;
        }

        public async Task<Document> CreateDocument(Document document)
        {
            string path = $"https://api.pdfmonkey.io/api/v1/documents";
            string json = JsonConvert.SerializeObject(document);

            var caller = new Caller<Document>(this.apiSecretKey);
            var result = await caller.PostAsync(path, json, Document.Identifier);

            return result;
        }

        public async Task DownloadPdfFile(Document document, string destination)
        {
            if (document.DownloadUrl == null)
                throw new Exception($"The Document have an empty download url!");

            var destinationPath = $"{destination}{document.Filename}";
            await DownloadFileHelper.DownloadFileAsync(document.DownloadUrl, destination);
        }

        public async Task DownloadPdfFile(DocumentCard card, string destination)
        {
            if (card.DownloadUrl == null)
                throw new Exception($"The Document have an empty download url!");

            var destinationPath = $"{destination}{card.Filename}";
            await DownloadFileHelper.DownloadFileAsync(card.DownloadUrl, destinationPath);
        }
    }
}