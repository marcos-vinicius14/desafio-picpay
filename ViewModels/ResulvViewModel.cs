using Microsoft.AspNetCore.Mvc;
using Picpay_01.Models;

namespace Picpay_01.ViewModels;

public class ResulvViewModel <T>
{
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public ResulvViewModel(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResulvViewModel(T data)
    {
        Data = data;
    }

    public ResulvViewModel(string error)
    {
        Errors.Add(error);
    }
    
}