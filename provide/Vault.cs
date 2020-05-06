using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace provide
{
    public class Vault: ApiClient
    {

        public Vault(string token) : base(token) {}

        public static Vault InitVault(string token) {
            return new Vault(token);
        }
        
        // CreateVault creates a new vault available within the key management service
        public async Task<(int, byte[])>CreateVault(string token, Dictionary<string, object> args) {
            var uri = String.Format("vaults");
            return await this.Post(uri, args);
        }

        // ListVaults lists available vaults available within the key management service
        public async Task<(int, byte[])>ListVaults(string token, Dictionary<string, object> args) {
            var uri = String.Format("vaults");
            return await this.Get(uri, args);
        }

        // ListVaultKeys retrieves a list of the symmetric keys and asymmetric key pairs secured within the key management service
        public async Task<(int, byte[])>ListVaultKeys(string token, string vaultID, Dictionary<string, object> args) {
            var uri = String.Format("vaults/{}", vaultID);
            return await this.Get(uri, args);
        }

        // CreateVault creates a new vault available within the key management service
        public async Task<(int, byte[])>CreateVaultKey(string token, string vaultID, Dictionary<string, object> args) {
            var uri = String.Format("vaults/{}/keys", vaultID);
            return await this.Post(uri, args);
        }

        // DeleteVaultKey permanently removes the specified key material from within the key management service
        public async Task<(int, byte[])>DeleteVaultKey(string token, string vaultID, string keyID) {
            var uri = String.Format("vaults/{}/keys/{}", vaultID, keyID);
            return await this.Delete(uri);
        }

        // SignMessage securely signs the given message
        public async Task<(int, byte[])>SignMessage(string token, string vaultID, string keyID, string msg) {
            var uri = String.Format("vaults/{}/keys/{}/sign", vaultID, keyID);
            return await this.Post(uri, new Dictionary<string, object> { { "message", msg } });
        }

        // VerifySignature verifies that a message was previously signed with a given key
        public async Task<(int, byte[])>VerifySignature(string token, string vaultID, string keyID, string msg, string sig) {
            var uri = String.Format("vaults/{}/keys/{}/verify", vaultID, keyID);
            return await this.Post(uri, new Dictionary<string, object> { { "message", msg }, { "signature", sig } });
        }
    }
}
