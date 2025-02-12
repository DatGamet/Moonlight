﻿using Moonlight.App.Exceptions;
using Moonlight.App.Repositories;
using Moonlight.App.Services.Sessions;
using OtpNet;

namespace Moonlight.App.Services;

public class TotpService
{
    private readonly IdentityService IdentityService;
    private readonly UserRepository UserRepository;

    public TotpService(
        IdentityService identityService, 
        UserRepository userRepository)
    {
        IdentityService = identityService;
        UserRepository = userRepository;
    }

    public Task<bool> Verify(string secret, string code)
    {
        var totp = new Totp(Base32Encoding.ToBytes(secret));
        var codeserver = totp.ComputeTotp();
        return Task.FromResult(codeserver == code);
    }

    public async Task<bool> GetEnabled()
    {
        var user = await IdentityService.Get();

        return user!.TotpEnabled;
    }

    public async Task<string> GetSecret()
    {
        var user = await IdentityService.Get();
        
        return user!.TotpSecret;
    }

    public async Task GenerateSecret()
    {
        var user = (await IdentityService.Get())!;
        
        user.TotpSecret = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));;
        
        UserRepository.Update(user);
    }

    public async Task Enable(string code)
    {
        var user = (await IdentityService.Get())!;

        if (!await Verify(user.TotpSecret, code))
        {
            throw new DisplayException("The 2fa code you entered is invalid");
        }

        user.TotpEnabled = true;
        UserRepository.Update(user);
    }

    public async Task Disable()
    {
        var user = (await IdentityService.Get())!;

        user.TotpEnabled = false;
        user.TotpSecret = "";

        UserRepository.Update(user);
        
        //TODO: AuditLog
    }
}